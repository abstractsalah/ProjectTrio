using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.DAL;
using AdvancedMortgageCalculator.Preferences;
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
        private BankDAO bankDAO;
        public BankService(BankDAO bankDAO)
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

            // Trouver le taux d'intérêt actuellement effectif
            var tauxTrouvé = (from rate in banqueTrouvée.MortgageInterestRates
                              where rate.Effective <= currentDate && rate.Expiry >= currentDate
                              orderby rate.Effective descending
                              select rate).FirstOrDefault();

            if (tauxTrouvé == null)
            {
                throw new NoBankOrRateFound($"Aucun taux d'intérêt valide n'a été trouvé pour la banque '{bankName}'.");
            }

            return tauxTrouvé;
        }
    }
    // Obtenir toutes les banques
    /*try
    {
        if (string.IsNullOrEmpty(bankName))
        {
            throw new ArgumentNullException("bankName", "Le nom de la banque ne peut pas être vide ou null.");
        }

        // Obtenir toutes les banques
        var banks = bankDAO.GetAllBanks();

        // Vérifier si nous avons des banques dans la liste
        if (banks == null)
        {
            throw new NoBankOrRateFound("Aucune banque n'est disponible dans le système.");
        }

        // Rechercher la banque par son nom avec LINQ


        // Trouver la banque par son nom en utilisant LINQ
        var banktrouve = from b in banks
                         where b.Name.ToLower().Equals(bankName.ToLower())
                         select b;

        Bank foundBank = banktrouve.FirstOrDefault();

        // Vérifier si la banque existe
        if (foundBank == null)
        {
            throw new NoBankOrRateFound($"La banque '{bankName}' n'a pas été trouvée.");
        }

        // Vérifier si la banque a des taux d'intérêt
        if (foundBank.MortgageInterestRates == null)
        {
            throw new NoBankOrRateFound($"Aucun taux d'intérêt n'est disponible pour la banque '{bankName}'.");
        }

        // Obtenir le taux d'intérêt le plus récent
        var currentDate = DateTime.Now;

        // Trouver le taux d'intérêt actuellement effectif en utilisant la syntaxe de requête
        var ratetrouve = from rate in foundBank.MortgageInterestRates
                         where rate.Effective <= currentDate && rate.Expiry >= currentDate
                         select rate;

        var currentRate = ratetrouve.FirstOrDefault();


        // Vérifier si nous avons trouvé un taux
        if (currentRate == null)
        {
            throw new NoBankOrRateFound($"Aucun taux d'intérêt valide n'a été trouvé pour la banque '{bankName}'.");
        }
        return currentRate;

    }
    catch (NoBankOrRateFound)
    {
        // Relancer l'exception personnalisée
        throw;
    }
    catch (Exception ex)
    {
        // Convertir les autres exceptions en NoBankFound
        throw new NoBankOrRateFound($"Une erreur s'est produite lors de la recherche du taux d'intérêt : {ex.Message}");
    }
}
}*/



}
