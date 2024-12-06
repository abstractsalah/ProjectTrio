using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
using MortageAmortizationService.BLL.Model;
using NSubstitute;
using RecurringCostService.BLL.Service;
using RecurringCostService.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MC_Tests
{
    public class UserStoryTests
    {
        [Fact]
        public void UserStoryTest_ShouldReturnCorrectResults()
        {
            // Arrange
            double housePrice = 300000;
            double initialDeposit = 50000;
            int months = 240;

            // Simuler BankService
            var fakeBankService = Substitute.For<IBankService>();
            fakeBankService.FetchBankWithLowestInterestRate().Returns(new Bank
            {
                Name = "Banque Nationale",
                MortgageInterestRates = new List<MortgageInterestRates>
        {
            new MortgageInterestRates(1, 2.5, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6))
        }
            });

            // Simuler LoanCalculatorService
            var fakeLoanCalculatorService = Substitute.For<ICalculatorService>();
            fakeLoanCalculatorService
                .CalculateMonthlyPayment("Banque Nationale", 250000, 240)
                .Returns(1321.00);

            fakeLoanCalculatorService
                .CalculateTotalAmountOfInterest("Banque Nationale", 250000, 240)
                .Returns(68540.00);

            // Service d'amortissement
            var amortizationService = new MortgageAmortizationService(fakeBankService, fakeLoanCalculatorService);

            // Act
            amortizationService.CalculateAmortization(housePrice, initialDeposit, months);

            // Assert
            // Vérifiez les valeurs attendues dans la sortie
        }
    }
}
