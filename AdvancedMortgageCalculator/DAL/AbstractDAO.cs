using MySql.Data.MySqlClient;
using AdvancedMortgageCalculator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.DAL
{
    public abstract class AbstractDAO
    {

        private readonly string connectionString;
        protected MySqlConnection Connection;
        public AbstractDAO
()
        {
            this.connectionString = $"Server={Preferences.host};Database={Preferences.dbname};"
                                    + $"Uid={Preferences.username};Pwd={Preferences.password};";
            this.Connection = new MySqlConnection(this.connectionString);
        }

        public MySqlConnection getConnection()
        {
            return this.Connection;
        }

    }
}
