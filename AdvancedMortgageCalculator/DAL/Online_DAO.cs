using AdvancedMortgageCalculator.BLL.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.DAL
{
    public class Online_DAO : AbstractDAO
    {
        public Online_DAO() : base() { }

            IList<Bank> FetchBankInterestRateByName(string bankName)
        {
            IList<Bank> allBanks = new List<Bank>();

            string sql = "SELECT b.id AS bank_id, b.name AS bank_name, b.address AS bank_address, p.id AS product_id, p.pn AS product_name, " +
                         "mir.id AS rate_id, mir.rate AS interest_rate, mir.effective_date AS interest_effective_date, mir.expiry_date AS interest_expiry_date " +
                         "FROM Bank b " +
                         "JOIN Product p ON p.bank_id = b.id " +
                         "LEFT JOIN MortgageInterestRates mir ON mir.bank_id = b.id " +
                         "WHERE b.name = @BankName;";

            this.Connection.Open();

            MySqlCommand command = new MySqlCommand(sql, this.Connection);
            command.Parameters.AddWithValue("@BankName", bankName);

            MySqlDataReader cursor = command.ExecuteReader();

            while (cursor.Read())
            {
                int bankId = cursor.GetInt32("bank_id");
                string bankNameResult = cursor.GetString("bank_name");
                string bankAddress = cursor.GetString("bank_address");

                // Création de la banque
                Bank bank = new Bank(bankId, bankNameResult, bankAddress, new List<Product>(), new List<MortgageInterestRates>());

                // Ajout du produit
                int productId = cursor.GetInt32("product_id");
                string productName = cursor.GetString("product_name");
                Product product = new Product(productId, productName);
                bank.Products.Add(product);

                // Ajout du taux d'intérêt s'il existe
                if (!cursor.IsDBNull(cursor.GetOrdinal("interest_rate")))
                {
                    int rateId = cursor.GetInt32("rate_id");
                    double rate = cursor.GetDouble("interest_rate");
                    DateTime effectiveDate = cursor.GetDateTime("interest_effective_date");
                    DateTime expiryDate = cursor.GetDateTime("interest_expiry_date");
                    MortgageInterestRates mortgageRate = new MortgageInterestRates(rateId, rate, effectiveDate, expiryDate);
                    bank.MortgageInterestRates.Add(mortgageRate);
                }

                // Ajouter la banque à la liste
                allBanks.Add(bank);
            }

            cursor.Close();
            this.Connection.Close();

            return allBanks;
        }
    }
}




