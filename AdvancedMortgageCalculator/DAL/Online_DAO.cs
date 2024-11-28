using AdvancedMortgageCalculator.BLL.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvancedMortgageCalculator.DAL
{
    public class Online_DAO : AbstractDAO
    {


        public Online_DAO() : base() { }

        public IList<Bank> FetchBankInterestRateByName(string bankName)
        {
            IList<Bank> allBanks = new List<Bank>();

            // Ouverture de la connexion
            this.Connection.Open();

            // Définir la commande pour appeler la procédure stockée
            MySqlCommand command = new MySqlCommand("FetchBankInterestRateByName", this.Connection);
            command.CommandType = CommandType.StoredProcedure;

            // Ajouter le paramètre de la procédure
            command.Parameters.AddWithValue("@BankName", bankName);

            // Exécuter la commande
            MySqlDataReader cursor = command.ExecuteReader();

            // Lire les résultats
            while (cursor.Read())
            {
                int bankId = cursor.GetInt32("bank_id");
                string bankNameResult = cursor.GetString("bank_name");
                string bankAddress = cursor.GetString("bank_address");

                // Création de la banque
                Bank bank = new Bank(bankId, bankNameResult, bankAddress, new List<MortgageInterestRates>());

                // Ajout du taux d'intérêt s'il existe
                if (!cursor.IsDBNull(cursor.GetOrdinal("rate_id")))
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

            // Fermer la connexion
            cursor.Close();
            this.Connection.Close();

            return allBanks;
        }

        public Bank GetBankWithLowestInterestRate()
        {
            Bank found = null;

            // Open the connection
            this.Connection.Open();

            // Create a command to call the stored procedure
            MySqlCommand command = new MySqlCommand("FetchBankWithLowestInterestRate", this.Connection);
            command.CommandType = CommandType.StoredProcedure;

            // Execute the command and read the result
            using (MySqlDataReader cursor = command.ExecuteReader())
            {
                if (cursor.Read())
                {
                    // Read bank details from the result set
                    string bankName = cursor.GetString("bank_name");
                    string bankAddress = cursor.GetString("bank_address");
                    double lowestRate = cursor.GetDouble("lowest_rate");
                    DateTime effectiveDate = cursor.GetDateTime("effective_date");
                    DateTime expiryDate = cursor.GetDateTime("expiry_date");

                    // Create a new Bank object and set its properties
                    found = new Bank
                    {
                        Name = bankName,
                        Address = bankAddress,
                        MortgageInterestRates = new List<MortgageInterestRates>
                {
                    new MortgageInterestRates
                    {
                        Rate = lowestRate,
                        Effective = effectiveDate,
                        Expiry = expiryDate
                    }
                }
                    };
                }
            }

            // Close the connection
            this.Connection.Close();

            return found;
        }

        public IList<Bank> FetchBanksByProductType(string productType)
        {
            IList<Bank> banks = new List<Bank>();

            // Ouvrir la connexion
            this.Connection.Open();

            // Créer une commande pour appeler la procédure stockée
            MySqlCommand command = new MySqlCommand("FetchBanksByProductType", this.Connection);
            command.CommandType = CommandType.StoredProcedure;

            // Ajouter le paramètre de la procédure
            command.Parameters.AddWithValue("@ProductType", productType);

            // Exécuter la commande et lire les résultats
            using (MySqlDataReader cursor = command.ExecuteReader())
            {
                while (cursor.Read())
                {
                    int bankId = cursor.GetInt32("bank_id");
                    string bankName = cursor.GetString("bank_name");
                    string bankAddress = cursor.GetString("bank_address");

                    // Ajouter la banque à la liste
                    banks.Add(new Bank(bankId, bankName, bankAddress, new List<Product>()));
                }
            }

            // Fermer la connexion
            this.Connection.Close();

            return banks;
        }
    }

}





