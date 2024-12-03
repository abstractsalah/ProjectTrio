using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
using MortageAmortizationService.BLL.Model;
using RecurringCostService.BLL.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace MortageAmortizationService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Mortgage Amortization Service ===");

            // Création des services des projets 1 et 2
            var interestRateService = new BankService(new BankDAO()); // IRS
            var loanCalculatorService = new LoanCalculatorService(new Online_DAO()); // RCS

            // Initialisation du service d'amortissement hypothécaire (MAS)
            var amortizationService = new MortgageAmortizationService(interestRateService, loanCalculatorService);

            // Collecte des entrées utilisateur
            Console.Write("Entrez le prix de la maison ($) : ");
            double housePrice = double.Parse(Console.ReadLine());

            Console.Write("Entrez l'apport initial ($) : ");
            double initialDeposit = double.Parse(Console.ReadLine());

            Console.Write("Entrez la durée de remboursement (en mois) : ");
            int months = int.Parse(Console.ReadLine());

            // Calculer et afficher les résultats
            amortizationService.CalculateAmortization(housePrice, initialDeposit, months);

            Console.WriteLine("all-OK!");

            Console.ReadKey();
        }
    }
}

