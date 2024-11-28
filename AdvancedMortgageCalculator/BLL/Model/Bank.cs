using System.Collections.Generic;

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

        public Bank() { }

        public Bank(int id, string name, string address, List<Product> products)
        {
            Id = id;
            Name = name;
            Address = address;
            Products = products;
        }
        public Bank(int id, string name, string address, List<MortgageInterestRates> mortgageInterestRates)
        {
            Id = id;
            Name = name;
            Address = address;
            MortgageInterestRates = mortgageInterestRates;
        }

        public Bank(string name, string address, List<MortgageInterestRates> mortgageInterestRates)
        {
            Name = name;
            Address = address;
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
