using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
using AdvancedMortgageCalculator.DL;
using System;

namespace MC_Tests
{
    public class P1_IRS
    {

        public class BankServiceTests
        {
            [Theory]
            [InlineData("Banque Nationale", 2.5)]
            [InlineData("Banque Internationale", 2.75)]
            public void GetBankInterestRateByName_Test(string bankName, double expectedRate)
            {
                // Arrange
                IBankDAO bankDAO = new BankDAO(); // Récupérer les données en mémoire
                var service = new BankService((BankDAO)bankDAO);

                // Act
                double actualRate = service.GetBankInterestRateByName(bankName);

                // Assert
                Assert.Equal(expectedRate, actualRate);
            }
        }

        [Theory]
        [InlineData("Banque Nationale", 2.5)]
        public void FetchBankWithLowestInterestRate_Test(string expectedBankName, double expectedRate)
        {
            // Arrange
            IBankDAO bankDAO = new BankDAO(); // Récupérer les données en mémoire
            var service = new BankService((BankDAO)bankDAO);

            // Act
            var bank = service.FetchBankWithLowestInterestRate();

            // Assert
            Assert.NotNull(bank);
            Assert.Equal(expectedBankName, bank.Name);
            Assert.Equal(expectedRate, bank.MortgageInterestRates.Min(rate => rate.Rate));
        }

        [Theory]
        [InlineData("Mortgage", new[] { "Banque Nationale", "Banque Internationale" })]
        [InlineData("Insurance", new[] { "Banque Nationale", "Banque Internationale" })]
        [InlineData("Loan", new[] { "Banque Internationale" })]
        public void FetchBanksByProductType_Test(string productType, string[] expectedBanks)
        {
            // Arrange
            IBankDAO bankDAO = new BankDAO(); // Récupérer les données en mémoire
            var service = new BankService((BankDAO)bankDAO);

            // Act
            var banks = service.GetBanksByProductType(productType);

            // Assert
            Assert.NotNull(banks);
            Assert.NotEmpty(banks);
            foreach (var expectedBank in expectedBanks)
            {
                Assert.Contains(banks, b => b.Name == expectedBank);
            }
        }
    }

    
}
