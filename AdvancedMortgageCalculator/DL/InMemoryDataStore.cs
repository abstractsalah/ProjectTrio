using AdvancedMortgageCalculator.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.DL
{
    public class InMemoryDataStore
    {
        public List<Bank> Banks { get; set; }
        private static InMemoryDataStore instance = null;

        private InMemoryDataStore()
        {
            Banks = new List<Bank>();

            // Création des listes pour la première banque
            var productsForBank1 = new List<Product>
        {
            new Product(1, "Compte Épargne"),
            new Product(2, "Carte de Crédit")
        };

            var interestRatesForBank1 = new List<MortgageInterestRates>
        {
            new MortgageInterestRates(1, 2.5, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)),
            new MortgageInterestRates(2, 3.0, new DateTime(2023, 2, 2), new DateTime(2023, 12, 31))
        };

            var insuranceRatesForBank1 = new List<MortgageInsuranceRates>
        {
            new MortgageInsuranceRates(1, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)),
            new MortgageInsuranceRates(2, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31))
        };

            // Ajout de la Banque Nationale avec ses listes
            Banks.Add(new Bank(
                id: 1,
                name: "Banque Nationale",
                address: "123 Rue Principale",
                products: productsForBank1,
                mortgageInterestRates: interestRatesForBank1,
                mortgageInsuranceRates: insuranceRatesForBank1));

            // Création des listes pour la deuxième banque
            var productsForBank2 = new List<Product>
        {
            new Product(3, "Compte Courant"),
            new Product(4, "Prêt Personnel")
        };

            var interestRatesForBank2 = new List<MortgageInterestRates>
        {
            new MortgageInterestRates(3, 2.75, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)),
            new MortgageInterestRates(4, 3.25, new DateTime(2023, 2, 2), new DateTime(2023, 12, 31))
        };

            var insuranceRatesForBank2 = new List<MortgageInsuranceRates>
        {
            new MortgageInsuranceRates(3, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)),
            new MortgageInsuranceRates(4, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31))
        };

            // Ajout de la Banque Internationale avec ses listes
            Banks.Add(new Bank(
                id: 2,
                name: "Banque Internationale",
                address: "456 Avenue du Monde",
                products: productsForBank2,
                mortgageInterestRates: interestRatesForBank2,
                mortgageInsuranceRates: insuranceRatesForBank2));
        }

        public static InMemoryDataStore GetInstance()
        {
            if (InMemoryDataStore.instance == null)
                InMemoryDataStore.instance = new InMemoryDataStore();
            return InMemoryDataStore.instance;
        }
    }


}