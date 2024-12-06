      using AdvancedMortgageCalculator.BLL.Model;
    using AdvancedMortgageCalculator.DAL;
using AdvancedMortgageCalculator.Utils;
using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.BLL.Service
{
    public class BankService : IBankService
    {
        IBankDAO bankDAO;
        public BankService(IBankDAO bankDAO)
        {
            this.bankDAO = bankDAO;
        }

        public double GetBankInterestRateByName(string bankName)
        {
            //Better version for unit testing :) //

            if (string.IsNullOrEmpty(bankName))
            {
                throw new ArgumentNullException(nameof(bankName), "Le nom de la banque ne peut pas être vide ou null.");
            }
            var banks = bankDAO.GetAllBanks();
            if (banks == null || !banks.Any())
            {
                throw new NoBankOrRateFound("Aucune banque n'est disponible dans le système.");
            }
            var banqueTrouvée = banks.FirstOrDefault(b => b.Name.Equals(bankName, StringComparison.OrdinalIgnoreCase));

            foreach (var rate in banqueTrouvée.MortgageInterestRates)
            {
                Console.WriteLine($"Taux: {rate.Rate}%, Début: {rate.Effective}, Fin: {rate.Expiry}");
            }

            if (banqueTrouvée == null)
            {
                throw new NoBankOrRateFound($"La banque '{bankName}' n'a pas été trouvée.");
            }

            if (banqueTrouvée.MortgageInterestRates == null || !banqueTrouvée.MortgageInterestRates.Any())
            {
                throw new NoBankOrRateFound($"Aucun taux d'intérêt n'est disponible pour la banque '{bankName}'.");
            }

            // Retourner le taux d'intérêt le plus bas
            double lowestRate = banqueTrouvée.MortgageInterestRates
                .Select(rate => rate.Rate)
                .Min();

            return lowestRate;
        }

                public double FetchBankInsuranceRateByName(string bankName)
        {
            var banks = bankDAO.GetAllBanks();
            var banqueTrouvée = (from b in banks
                                 where b.Name.Equals(bankName, StringComparison.OrdinalIgnoreCase)
                                 select b).FirstOrDefault();
            var currentDate = DateTime.Now;
            Console.WriteLine($"Date actuelle : {currentDate}");
            foreach (var rate in banqueTrouvée.MortgageInsuranceRates)
            {
                Console.WriteLine($"Taux: {rate.Rate}%, Début: {rate.Effective}, Fin: {rate.Expiry}");
            }

            var tauxTrouvé = (from rate in banqueTrouvée.MortgageInsuranceRates
                              orderby rate.Effective descending
                              select rate).FirstOrDefault();
            return tauxTrouvé.Rate;
        }

        public Bank FetchBankWithLowestInterestRate()
        {
            IList<Bank> banks = bankDAO.GetAllBanks();


            var bankWithLowestRate = banks
                .OrderBy(b => b.MortgageInterestRates.Min(m => m.Rate)) // Trier par le plus bas taux
                .FirstOrDefault(); // Prendre la première banque avec le taux le plus bas

            return bankWithLowestRate;
        }

        public IList<Bank> GetBanksByProductType(string productType)
        {
            
            IList<Bank> banks = bankDAO.GetAllBanks();
            var banksWithProductType = banks
                .Where(bank => bank.Products.Any(product => product.Type.Equals(productType, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return banksWithProductType;
        }

    }
}
