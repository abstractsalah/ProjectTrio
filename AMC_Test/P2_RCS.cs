using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.DAL;
using NSubstitute;
using RecurringCostService.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Tests
{
    public class LoanCalculatorServiceTests
    {
        [Theory]
        [InlineData("Banque Nationale", 100000, 60, 1841.65)] // Prêt de 100k sur 60 mois
        [InlineData("Banque Internationale", 150000, 120, 1518.68)] // Prêt de 150k sur 10 ans

        /*public void CalculateMonthlyPayment_ShouldReturnCorrectValue(string bankName, double loanAmount, int months, double expectedPayment)
        {
            // Arrange
            var onlineDao = new Online_DAO(); 
            var service = new LoanCalculatorService(onlineDao);

            // Act
            double actualPayment = service.CalculateMonthlyPayment(bankName, loanAmount, months);

            // Assert
            Assert.Equal(expectedPayment, actualPayment, 2);
        }
        [Theory]
        [InlineData("Banque Nationale", 100000, 60, 9823.51)] 
        [InlineData("Banque Internationale", 150000, 120, 36549.19)] 
        public void CalculateTotalAmountOfInterest_ShouldReturnCorrectValue(string bankName, double loanAmount, int months, double expectedInterest)
        {
            // Arrange
            var onlineDao = new Online_DAO(); // Source de données en mémoire
            var service = new LoanCalculatorService(onlineDao);

            // Act
            double actualInterest = service.CalculateTotalAmountOfInterest(bankName, loanAmount, months);

            // Assert
            Assert.Equal(expectedInterest, actualInterest, 2);
        }
    }*/
        // Malheureusement la premiere ne peut pas marcher parce qu'il rencontre un probleme dans l'ouverture de connexion avec online_dao :/ donc je mock l'online dao

        public void CalculateMonthlyPayment_Test(string bankName, double loanAmount, int months, double expectedPayment)
        {
            // Arrange
            var mockDao = Substitute.For<IOnlineDAO>();

            // Simuler les taux d'intérêt et d'assurance
            mockDao.FetchBankInterestRateByName(bankName).Returns(new Bank
            {
                Name = bankName,
                MortgageInterestRates = new List<MortgageInterestRates>
        {
            new MortgageInterestRates(1, 3.5, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6)) // addmonths sert ici a manipuler 6 moi de la date d'aujordhui pour avoir effective and expiry date 
        }
            });

            mockDao.FetchMortgageInsuranceRateByBank(bankName).Returns(new List<MortgageInsuranceRates>
    {
        new MortgageInsuranceRates(1, 0.5, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6))   
    });

            var service = new LoanCalculatorService(mockDao);

            // Act
            double actualPayment = service.CalculateMonthlyPayment(bankName, loanAmount, months);

            // Assert
            Assert.Equal(expectedPayment, actualPayment, 2);     // dernier arguement est int precision pour la précision de 2 décimales
        }


        [Theory]
        [InlineData("Banque Nationale", 100000, 60, 10499.13)] // Intérêt pour 100k sur 60 mois
        [InlineData("Banque Internationale", 150000, 120, 32241.25)] // Intérêt pour 150k sur 10 ans
        public void CalculateTotalAmountOfInterest_Test(string bankName, double loanAmount, int months, double expectedInterest)
        {
            // Arrange
            var mockDao = Substitute.For<IOnlineDAO>();

            // Simuler les taux d'intérêt pour les banques
            mockDao.FetchBankInterestRateByName(bankName).Returns(new Bank
            {
                Name = bankName,
                MortgageInterestRates = new List<MortgageInterestRates>
        {
            new MortgageInterestRates(1, 3.5, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6))
        }
            });

            // Simuler les taux d'assurance pour les banques
            mockDao.FetchMortgageInsuranceRateByBank(bankName).Returns(new List<MortgageInsuranceRates>
    {
        new MortgageInsuranceRates(1, 0.5, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6))
    });

            var service = new LoanCalculatorService(mockDao);

            // Act
            double actualInterest = service.CalculateTotalAmountOfInterest(bankName, loanAmount, months);

            // Assert
            Assert.Equal(expectedInterest, actualInterest, 2); // Précision de 2 décimales
        }

    }
}
