using AdvancedMortgageCalculator.DAL;
using RecurringCostService.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCostService
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("=== Loan Calculator Service ===");

            // Création d'une instance d'Online_DAO 
            var onlineDao = new Online_DAO();

            // Injection du DAO dans LoanCalculatorService
            var loanCalculatorService = new LoanCalculatorService(onlineDao);

            // Collecter les entrées de l'utilisateur
            Console.Write("Entrez le nom de la banque : ");
            string bankName = Console.ReadLine();

            Console.Write("Entrez le montant du prêt ($) : ");
            double loanAmount = double.Parse(Console.ReadLine());

            Console.Write("Entrez la durée du prêt (en mois) : ");
            int months = int.Parse(Console.ReadLine());

            try
            {
                // Calculer le paiement mensuel
                double monthlyPayment = loanCalculatorService.CalculateMonthlyPayment(bankName, loanAmount, months);
                Console.WriteLine($"Le montant du versement mensuel est de : {Math.Round(monthlyPayment, 2)} $");

                // Calculer le montant total des intérêts payés
                double totalInterest = loanCalculatorService.CalculateTotalAmountOfInterest(bankName, loanAmount, months);
                Console.WriteLine($"Le montant total des intérêts payés est de : {Math.Round(totalInterest, 2)} $");
            }
            catch (Exception ex)
            {
                // Gérer les erreurs possibles
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
