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
    public class BankService

        
    {
        IBankDAO bankDAO;
        public BankService(IBankDAO bankDAO)
        {
            this.bankDAO = bankDAO;
        }

        public Object GetBankInterestRateByName(string bankName)
        {
            if (string.IsNullOrEmpty(bankName))
            {
                throw new ArgumentNullException(nameof(bankName), "Le nom de la banque ne peut pas être vide ou null.");
            }

            // Obtenir toutes les banques
            var banks = bankDAO.GetAllBanks();
            if (banks == null || !banks.Any())
            {
                throw new NoBankOrRateFound("Aucune banque n'est disponible dans le système.");
            }

            // Trouver la banque par son nom
            var banqueTrouvée = (from b in banks
                                 where b.Name.Equals(bankName, StringComparison.OrdinalIgnoreCase)
                                 select b).FirstOrDefault();

            if (banqueTrouvée == null)
            {
                throw new NoBankOrRateFound($"La banque '{bankName}' n'a pas été trouvée.");
            }

            if (banqueTrouvée.MortgageInterestRates == null || !banqueTrouvée.MortgageInterestRates.Any())
            {
                throw new NoBankOrRateFound($"Aucun taux d'intérêt n'est disponible pour la banque '{bankName}'.");
            }

            var currentDate = DateTime.Now;
            Console.WriteLine($"Date actuelle : {currentDate}");

            // Afficher tous les taux disponibles pour le débogage
            foreach (var rate in banqueTrouvée.MortgageInterestRates)
            {
                Console.WriteLine($"Taux: {rate.Rate}%, Début: {rate.Effective}, Fin: {rate.Expiry}");
            }

            // Trouver le taux d'intérêt le plus récent sans vérification de date
            var tauxTrouvé = (from rate in banqueTrouvée.MortgageInterestRates
                              orderby rate.Effective descending
                              select rate).FirstOrDefault();

            if (tauxTrouvé == null)
            {
                throw new NoBankOrRateFound($"Aucun taux d'intérêt valide n'a été trouvé pour la banque '{bankName}'.");
            }

            return tauxTrouvé;
        }
    }
}
