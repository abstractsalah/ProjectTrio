using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.BLL.Service;
using AdvancedMortgageCalculator.DAL;
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

            /*Online_DAO online_DAO = new Online_DAO();
            IEnumerable<Bank> banks = online_DAO.FetchBankInterestRateByName("Banque Nationale");
            foreach (var bank in banks)
            {
                Console.WriteLine($"Bank Name: {bank.Name}");
                foreach (var product in bank.Products)
                {
                    Console.WriteLine($" - Product: {product.Type}");
                }
                foreach (var rate in bank.MortgageInterestRates)
                {
                    Console.WriteLine($" - Interest Rate: {rate.Rate}% effective from {rate.Effective.ToShortDateString()} to {rate.Expiry.ToShortDateString()}");
                }
            }
            Console.ReadKey();
        }
    }
}*/

            Offline_DAO offline_DAO = new Offline_DAO();
            IEnumerable<Bank> banks = offline_DAO.FetchBankInterestRateByName_Offline("Banque Nationale");
            foreach (var bank in banks)
            {
                Console.WriteLine($"Bank Name: {bank.Name}");
                foreach (var product in bank.Products)
                {
                    Console.WriteLine($" - Product: {product.Type}");
                }
                foreach (var rate in bank.MortgageInterestRates)
                {
                    Console.WriteLine($" - Interest Rate: {rate.Rate}% effective from {rate.Effective.ToShortDateString()} to {rate.Expiry.ToShortDateString()}");
                }
            }
            Console.ReadKey();
        }
    }
}




/*Console.WriteLine("Entrez le nom de la banque :");
string bankName = Console.ReadLine();

Online_DAO online_DAO = new Online_DAO();
IList<Bank> banks = online_DAO.FetchBankInterestRateByName(bankName);

if (banks == null || banks.Count == 0)
{
    Console.WriteLine($"Aucune banque trouvée avec le nom '{bankName}'.");
}
else
{
    foreach (var bank in banks)
    {
        if (bank.MortgageInterestRates != null && bank.MortgageInterestRates.Count > 0)
        {
            foreach (var mortgageRate in bank.MortgageInterestRates)
            {
                Console.WriteLine($"Le taux d'intérêt annuel pour {bank.Name} est de {mortgageRate.Rate}% (effectif du {mortgageRate.Effective.ToShortDateString()} au {mortgageRate.Expiry.ToShortDateString()}).");
            }
        }
        else
        {
            Console.WriteLine($"Aucun taux d'intérêt trouvé pour la banque '{bank.Name}'.");
        }
    }
}

Console.ReadKey();
}
}
}*/
            // var bankDAO = new BankDAO();
            //var bankService = new BankService(bankDAO);
            //var rate = bankService.GetBankInterestRateByName("Banque Internationale");
            //Console.ReadKey();
        //}
    //}
//}
