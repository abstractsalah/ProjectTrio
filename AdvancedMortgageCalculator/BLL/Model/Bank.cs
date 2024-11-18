using Org.BouncyCastle.Utilities.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdvancedMortgageCalculator.BLL.Model
{
    public class Bank
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Product> Products { get; set; }
        public List<MortgageInterestRates> MortgageInterestRates { get; set; }
        public List<MortgageInsuranceRates> MortgageInsuranceRates { get; set; }
        public Bank(int id, string name, string address, List<Product> products, List<MortgageInterestRates> mortgageInterestRates)
        {
            Id = id;
            Name = name;
            Address = address;
            Products = products;
            MortgageInterestRates = mortgageInterestRates;
        }

        public Bank(int id, string name, string address, List<Product> products, List<MortgageInterestRates> mortgageInterestRates, List<MortgageInsuranceRates> mortgageInsuranceRates)
        {
            Id = id;
            Name = name;
            Address = address;
            Products = products;
            MortgageInterestRates = mortgageInterestRates;
            MortgageInsuranceRates = mortgageInsuranceRates;
        }



        public override string ToString()
        {
            string productsString = string.Join(", ", Products);
            string interestRatesString = string.Join(", ", MortgageInterestRates);
            string insuranceRatesString = string.Join(", ", MortgageInsuranceRates);

            return $"Bank [Id={Id}, Name={Name}, Address={Address}, Products=[{productsString}], MortgageInterestRates=[{interestRatesString}], MortgageInsuranceRates=[{insuranceRatesString}]]";
        }
    }
}
