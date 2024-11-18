using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.DAL;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;

public class Offline_DAO : AbstractDAO
{
    MySqlDataAdapter DataAdapter;
    public DataSet DataSet { get; set; }

    public Offline_DAO() : base() { }

    public IList<Bank> FetchBankInterestRateByName_Offline(string bankName)
    {
        this.DataSet = new DataSet();

        string sql = "SELECT b.id AS bank_id, " +
                    "b.name AS bank_name, " +
                    "b.address AS bank_address, " +
                    "mir.id AS rate_id, " +
                    "mir.rate AS interest_rate, " +
                    "mir.effective_date AS interest_effective_date, " +
                    "mir.expiry_date AS interest_expiry_date " +
                    "FROM Bank b " +
                    "LEFT JOIN MortgageInterestRates mir ON mir.bank_id = b.id " +
                    "WHERE b.name = @BankName";

        this.Connection.Open();
        this.DataAdapter = new MySqlDataAdapter(sql, (MySqlConnection)this.Connection);
        this.DataAdapter.SelectCommand.Parameters.AddWithValue("@BankName", bankName);
        this.DataAdapter.Fill(this.DataSet, "Bank_Interest");

        List<Bank> banks = new List<Bank>();
        DataTable bankTable = this.DataSet.Tables["Bank_Interest"];

        // Créer une seule banque avec ses taux d'intérêt
        if (bankTable.Rows.Count > 0)
        {
            DataRow firstRow = bankTable.Rows[0];
            int bankId = Convert.ToInt32(firstRow["bank_id"]);
            string bankNameValue = firstRow["bank_name"].ToString();
            string bankAddress = firstRow["bank_address"].ToString();

            List<MortgageInterestRates> rates = new List<MortgageInterestRates>();

            // Parcourir toutes les lignes pour obtenir les taux
            foreach (DataRow row in bankTable.Rows)
            {
                if (row["rate_id"] != DBNull.Value)
                {
                    MortgageInterestRates rate = new MortgageInterestRates();
                    rate.Id = Convert.ToInt32(row["rate_id"]);

                    if (row["interest_rate"] != DBNull.Value)
                    {
                        rate.Rate = Convert.ToDouble(row["interest_rate"]);
                    }
                    else
                    {
                        rate.Rate = 0.0d;
                    }

                    if (row["interest_effective_date"] != DBNull.Value)
                    {
                        rate.Effective = Convert.ToDateTime(row["interest_effective_date"]);
                    }
                    else
                    {
                        rate.Effective = DateTime.MinValue;
                    }

                    if (row["interest_expiry_date"] != DBNull.Value)
                    {
                        rate.Expiry = Convert.ToDateTime(row["interest_expiry_date"]);
                    }
                    else
                    {
                        rate.Expiry = DateTime.MaxValue;
                    }

                    rates.Add(rate);
                }
            }

            Bank bank = new Bank(
                bankId,
                bankNameValue,
                bankAddress,
                new List<Product>(),  // Liste vide de produits
                rates
            );

            banks.Add(bank);
        }

        if (this.Connection.State == ConnectionState.Open)
        {
            this.Connection.Close();
        }

        return banks;
    }

    private static Offline_DAO Singleton = null;
    public static Offline_DAO getInstance()
    {
        if (Offline_DAO.Singleton == null)
            Offline_DAO.Singleton = new Offline_DAO();
        return Offline_DAO.Singleton;
    }
}