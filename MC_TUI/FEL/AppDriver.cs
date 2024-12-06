using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
using MortageAmortizationService.BLL.Model;
using RecurringCostService.BLL.Service;
using RecurringCostService.DAL;
using System;

namespace MC_TUI
{
    public class AppDriver
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" MAS in progress ...");

            bool isSuccess = false;
            IBankDAO bankDAO = new BankDAO();
            IBankService bankService = new BankService(bankDAO);
            ICalculatorService loanCalculatorService = new LoanCalculatorService(new Online_DAO()); // RCS

            var amortizationService = new MortgageAmortizationService(bankService, loanCalculatorService);


            // Collecte des entrées utilisateur
            while (!isSuccess)
            {
                try
                {
                    Console.Write("Entrez le prix de la maison ($) : ");
            double housePrice = double.Parse(Console.ReadLine());

            Console.Write("Entrez l'apport initial ($) : ");
            double initialDeposit = double.Parse(Console.ReadLine());

            Console.Write("Entrez la durée de remboursement (en mois) : ");
            int months = int.Parse(Console.ReadLine());

            // Calculer et afficher les résultats
            amortizationService.CalculateAmortization(housePrice, initialDeposit, months);
                    isSuccess = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur : Veuillez entrer un nombre valide pour les montants et la durée.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une erreur inattendue s'est produite : {ex.Message}");
                }

                // Demander à l'utilisateur s'il veut réessayer
                if (!isSuccess)
                {
                    Console.Write("Voulez-vous essayer à nouveau ? (o/n) : ");
                    string retry = Console.ReadLine().ToLower();
                    if (retry != "o")
                    {
                        Console.WriteLine("On va fermer alors !");
                        break;
                    }
                }
            }

            Console.WriteLine("Program is closing ...");

            Console.ReadKey();
        }
    }
}