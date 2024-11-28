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

            this.Connection.Open();
            MySqlCommand command = new MySqlCommand("FetchBankInterestRateByName", this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@BankName", bankName);
            MySqlDataReader cursor = command.ExecuteReader();

            while (cursor.Read())
            {
                int bankId = cursor.GetInt32("bank_id");
                string bankNameResult = cursor.GetString("bank_name");
                string bankAddress = cursor.GetString("bank_address");

                Bank bank = new Bank(bankId, bankNameResult, bankAddress, new List<MortgageInterestRates>());
                if (!cursor.IsDBNull(cursor.GetOrdinal("rate_id")))
                {
                    int rateId = cursor.GetInt32("rate_id");
                    double rate = cursor.GetDouble("interest_rate");
                    DateTime effectiveDate = cursor.GetDateTime("interest_effective_date");
                    DateTime expiryDate = cursor.GetDateTime("interest_expiry_date");
                    MortgageInterestRates mortgageRate = new MortgageInterestRates(rateId, rate, effectiveDate, expiryDate);
                    bank.MortgageInterestRates.Add(mortgageRate);
                }
                allBanks.Add(bank);
            }
            cursor.Close();
            this.Connection.Close();

            return allBanks;
        }

        public Bank GetBankWithLowestInterestRate()
        {
            Bank found = null;

            this.Connection.Open();

            // the command is going to call the stored procedure instead of using the long string sql query^^
            MySqlCommand command = new MySqlCommand("FetchBankWithLowestInterestRate", this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            using (MySqlDataReader cursor = command.ExecuteReader())
            {
                if (cursor.Read())
                {
                    string bankName = cursor.GetString("bank_name");
                    string bankAddress = cursor.GetString("bank_address");
                    double lowestRate = cursor.GetDouble("lowest_rate");
                    DateTime effectiveDate = cursor.GetDateTime("effective_date");
                    DateTime expiryDate = cursor.GetDateTime("expiry_date");
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
            this.Connection.Close();

            return found;
        }

        public IList<Bank> FetchBanksByProductType(string productType)
        {
            IList<Bank> banks = new List<Bank>();

            this.Connection.Open();
            MySqlCommand command = new MySqlCommand("FetchBanksByProductType", this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductType", productType);
            using (MySqlDataReader cursor = command.ExecuteReader())
            {
                while (cursor.Read())
                {
                    int bankId = cursor.GetInt32("bank_id");
                    string bankName = cursor.GetString("bank_name");
                    string bankAddress = cursor.GetString("bank_address");

                    banks.Add(new Bank(bankId, bankName, bankAddress, new List<Product>()));
                }
            }
            this.Connection.Close();

            return banks;
        }
    }

}





