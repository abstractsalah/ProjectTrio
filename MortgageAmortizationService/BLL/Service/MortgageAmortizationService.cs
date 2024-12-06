using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
using RecurringCostService.BLL.Service;
using RecurringCostService.DAL;

namespace MortageAmortizationService.BLL.Model
{


        // Injecter les dépendances 
        public class MortgageAmortizationService
        {
            private readonly IBankService bankService;
            private readonly ICalculatorService loanCalculatorService;

            public MortgageAmortizationService(IBankService bankService, ICalculatorService loanCalculatorService)
            {
                this.bankService = bankService;
                this.loanCalculatorService = loanCalculatorService;
            }
            public void CalculateAmortization(double housePrice, double initialDeposit, int months)
        {
            try
            {
                // Étape 1 : Calcul du montant du prêt
                double loanAmount = housePrice - initialDeposit;
                if (loanAmount == 0)
                {
                    Console.WriteLine("Erreur : L'apport initial est  égal au prix de la maison.");
                    return;

                }
                else if (loanAmount < 0) 
                {
                    Console.WriteLine("Erreur : Tu n'est pas eligible pour un prêt :P .");
                    return;

                }


                // Étape 2 : Obtenir le meilleur taux hypothécaire via InterestRateService (IRS)
                var bestRateBank = bankService.FetchBankWithLowestInterestRate();
                

                // Étape 3 : Calculer le paiement mensuel et les intérêts via RecurringCostService (RCS)
                double monthlyPayment = loanCalculatorService.CalculateMonthlyPayment(
                    bestRateBank.Name,
                    loanAmount,
                    months
                );
                double totalInterest = loanCalculatorService.CalculateTotalAmountOfInterest(
                    bestRateBank.Name,
                    loanAmount,
                    months
                );

                // Étape 4 : Afficher les résultats
                Console.WriteLine("\n=== Résultats de l'amortissement hypothécaire ===");
                Console.WriteLine($"\nMeilleur taux hypothécaire trouvé : {bestRateBank.MortgageInterestRates.Min(m => m.Rate) / 100}%");
                Console.WriteLine($"Nom de la banque : {bestRateBank.Name}");
                Console.WriteLine($"Montant du prêt : {loanAmount}$");
                Console.WriteLine($"Durée : {months} mois");
                Console.WriteLine($"Paiement mensuel : {Math.Round(monthlyPayment, 2)}$");
                Console.WriteLine($"Total des intérêts payés : {Math.Round(totalInterest, 2)}$");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}

