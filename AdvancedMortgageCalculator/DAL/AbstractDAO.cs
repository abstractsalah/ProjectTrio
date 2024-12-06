using AdvancedMortgageCalculator.Utils;
using MySql.Data.MySqlClient;

namespace AdvancedMortgageCalculator.DAL
{
    public abstract class AbstractDAO
    {

        private readonly string connectionString;
        protected MySqlConnection Connection;
        public AbstractDAO()
        {
            this.connectionString = $"Server={Preferences.host};Database={Preferences.dbname};"
                                    + $"Uid={Preferences.username};Pwd={Preferences.password};";
            this.Connection = new MySqlConnection(this.connectionString);
        }
    }
}
