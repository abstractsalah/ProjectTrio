using AdvancedMortgageCalculator.BLL.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvancedMortgageCalculator.DAL
{
    public class Online_DAO : AbstractDAO , IOnlineDAO
    {


        public Online_DAO() : base() { }

        public Bank FetchBankInterestRateByName(string bankName)
        {
            Bank bank = null;

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

                if (bank == null)
                {
                    bank = new Bank(bankId, bankNameResult, bankAddress, new List<MortgageInterestRates>());
                }

                if (!cursor.IsDBNull(cursor.GetOrdinal("rate_id")))
                {
                    int rateId = cursor.GetInt32("rate_id");
                    double rate = cursor.GetDouble("interest_rate");
                    DateTime effectiveDate = cursor.GetDateTime("interest_effective_date");
                    DateTime expiryDate = cursor.GetDateTime("interest_expiry_date");
                    MortgageInterestRates mortgageRate = new MortgageInterestRates(rateId, rate, effectiveDate, expiryDate);
                    bank.MortgageInterestRates.Add(mortgageRate);
                }
            }

            cursor.Close();
            this.Connection.Close();

            return bank;
        }


        public Bank GetBankWithLowestInterestRate()
        {
            Bank found = null;

            this.Connection.Open();
             //stored procedure incoming ...
            MySqlCommand command = new MySqlCommand("FetchBankWithLowestInterestRate", this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataReader cursor = command.ExecuteReader();
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
                    new MortgageInterestRates {Rate = lowestRate,Effective = effectiveDate,Expiry = expiryDate
                   }

                }
                    };
               }
            }
            cursor.Close();
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
            MySqlDataReader cursor = command.ExecuteReader();
            {
                while (cursor.Read())
                {
                    int bankId = cursor.GetInt32("bank_id");
                    string bankName = cursor.GetString("bank_name");
                    string bankAddress = cursor.GetString("bank_address");

                    banks.Add(new Bank(bankId, bankName, bankAddress, new List<Product>()));
                }
            }
            cursor.Close();
            this.Connection.Close();

            return banks;
        }

        public IList<MortgageInsuranceRates> FetchMortgageInsuranceRateByBank(string bankName)
        {
            IList<MortgageInsuranceRates> insuranceRates = new List<MortgageInsuranceRates>();

            this.Connection.Open();
            MySqlCommand command = new MySqlCommand("GetInsuranceRatesByBank", this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@BankName", bankName);
            MySqlDataReader cursor = command.ExecuteReader();
            {
                while (cursor.Read())
                {
                    int rateId = cursor.GetInt32("rate_id");
                    double rate = cursor.GetDouble("rate");
                    DateTime effectiveDate = cursor.GetDateTime("effective_date");
                    DateTime expiryDate = cursor.GetDateTime("expiry_date");
                    MortgageInsuranceRates insuranceRate = new MortgageInsuranceRates(rateId, rate, effectiveDate, expiryDate);
                    insuranceRates.Add(insuranceRate);
                }
            }
            cursor.Close();
            this.Connection.Close();

            return insuranceRates;
        }

    }

}





