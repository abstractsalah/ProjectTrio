using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 1st user story using onlinedao v1 //

            /*Online_DAO online_DAO = new Online_DAO();                                                                                      
             Bank found = online_DAO.FetchBankInterestRateByName("Banque Nationale");
                 Console.WriteLine($"Bank Name: {found.Name}");
                 foreach (var rate in found.MortgageInterestRates)
                 {
                     Console.WriteLine($" - Interest Rate: {rate.Rate}% effective from {rate.Effective.ToShortDateString()} to {rate.Expiry.ToShortDateString()}");
                 }
            Console.ReadKey();
        }
             
         }
     }*/





            /*Online_DAO dao = new Online_DAO();
            Console.WriteLine("Entrez le nom de la banque :");
            string bankName = Console.ReadLine();
            Bank bank = dao.FetchBankInterestRateByName(bankName);
            if (bank != null)
            {
                Console.WriteLine($"Banque trouvée : {bank.Name} ({bank.Address})");

                if (bank.MortgageInterestRates.Count > 0)
                {
                    Console.WriteLine("Taux d'intérêt hypothécaires :");
                    foreach (var rate in bank.MortgageInterestRates)
                    {
                        Console.WriteLine($"- {rate.Rate}% (Effective: {rate.Effective.ToShortDateString()} - Expiry: {rate.Expiry.ToShortDateString()})");
                    }
                }
                else
                {
                    Console.WriteLine("Aucun taux d'intérêt disponible pour cette banque.");
                }
            }
            else
            {
                Console.WriteLine($"Aucune banque trouvée avec le nom '{bankName}'.");
            }
            Console.ReadKey();
        }
    }
}*/


            /*Console.WriteLine("Entrez le nom de la banque :");                        // 1st user story using service //
            IBankDAO bankDAO = new BankDAO();
            BankService bankService = new BankService((BankDAO)bankDAO);
            string bankName = Console.ReadLine();
            object interestRate = bankService.GetBankInterestRateByName(bankName);
            Console.ReadKey(); 
            }
            }

            }*/


            /* IBankDAO bankDAO = new BankDAO();                                                  // 2nd user story using service //            
             BankService bankService = new BankService((BankDAO)bankDAO);

             // Appel de la méthode pour trouver la banque avec le taux d'intérêt le plus bas
             Bank bankWithLowestRate = bankService.FetchBankWithLowestInterestRate();
              {
                  var lowestRate = bankWithLowestRate.MortgageInterestRates.Min(m => m.Rate);
                  Console.WriteLine($"La banque avec le taux d'intérêt le plus bas est : {bankWithLowestRate.Name}");
                  Console.WriteLine($"Adresse : {bankWithLowestRate.Address}");
                  Console.WriteLine($"Taux d'intérêt : {lowestRate}%");
                  Console.ReadKey();
              }
            }
            }
            }*/

            /*Online_DAO online_DAO = new Online_DAO();                                          // 2nd user story online //
            Bank found = online_DAO.GetBankWithLowestInterestRate();
            {
                var lowestRate = found.MortgageInterestRates.First().Rate;
                var effectiveDate = found.MortgageInterestRates.First().Effective.ToShortDateString();
                var expiryDate = found.MortgageInterestRates.First().Expiry.ToShortDateString();

                Console.WriteLine($"La banque qui offre le taux d'interet le plus bas est: {found.Name}");
                Console.WriteLine($"Address: {found.Address}");
                Console.WriteLine($"Interest Rate: {lowestRate}% (Effective: {effectiveDate} - Expiry: {expiryDate})");
            }
            Console.ReadKey();
            }
            }
            }*/

            /* Online_DAO online_DAO = new Online_DAO();                                       // 3rd user story online //
             Console.WriteLine("Entrez le type de produit (Mortgage, Insurance, Savings_Account, Checking_Account, Loan) :");
             string productType = Console.ReadLine();
             IList<Bank> banks = online_DAO.FetchBanksByProductType(productType);
             if (banks.Count > 0)
             {
                 Console.WriteLine($"Les banques qui offrent le produit '{productType}' sont :");
                 foreach (var bank in banks)
                 {
                     Console.WriteLine($"- {bank.Name} (Adresse : {bank.Address})");
                 }
             }
             Console.ReadKey();
            }
            }
            }*/
            /* IBankDAO bankDAO = new BankDAO();                                                  // 2nd user story using service //            
             BankService bankService = new BankService((BankDAO)bankDAO);
             Console.WriteLine("Entrez le type de produit (Mortgage, Insurance, Savings_Account, Checking_Account, Loan) :");
             string productType = Console.ReadLine();

             // Appeler la méthode pour récupérer les banques
             var banks = bankService.GetBanksByProductType(productType);

             // Afficher les résultats
             if (banks.Count > 0)
             {
                 Console.WriteLine($"Les banques qui offrent le produit '{productType}' sont :");
                 foreach (var bank in banks)
                 {
                     Console.WriteLine($"- {bank.Name} (Adresse : {bank.Address})");
                 }
             }
             else
             {
                 Console.WriteLine($"Aucune banque ne propose le produit '{productType}'.");
             }
             Console.ReadKey();
            }
            }
            }*/

            /*Console.WriteLine("Entrez le nom de la banque :");                        // INSURANCERATE using service //
            IBankDAO bankDAO = new BankDAO();
            BankService bankService = new BankService((BankDAO)bankDAO);
            string bankName = Console.ReadLine();
            object interestRate = bankService.FetchBankInsuranceRateByName(bankName);
            Console.ReadKey();
        }
    }
}*/


            // Instancier le DAO
            Online_DAO dao = new Online_DAO();

            // Demander à l'utilisateur d'entrer le nom de la banque
            Console.WriteLine("Entrez le nom de la banque pour récupérer les taux d'assurance hypothécaire :");
            string bankName = Console.ReadLine();

            // Appeler la méthode
            IList<MortgageInsuranceRates> insuranceRates = dao.FetchMortgageInsuranceRateByBank(bankName);

            // Afficher les résultats
            if (insuranceRates.Count > 0)
            {
                Console.WriteLine($"Taux d'assurance hypothécaire pour '{bankName}':");
                foreach (var rate in insuranceRates)
                {
                    Console.WriteLine($"- Rate: {rate.Rate}%, Effective: {rate.Effective.ToShortDateString()}, Expiry: {rate.Expiry.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine($"Aucun taux d'assurance hypothécaire trouvé pour la banque '{bankName}'.");
            }
            Console.ReadKey();
        }
    }
}
