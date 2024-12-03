using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.BLL.Model
{
    public class MortgageInsuranceRates
    {
        public static int _AUTO_GEN = 1;
        public int Id { get; set; }
        public double Rate { get; set; }
        public DateTime Effective { get; set; }
        public DateTime Expiry { get; set; }

        public MortgageInsuranceRates() 
        {
            this.Id = MortgageInsuranceRates._AUTO_GEN++;
            
        }

        public MortgageInsuranceRates(int id, double rate, DateTime effective, DateTime expiry)
        {
            Id = id;
            Rate = rate;
            Effective = effective;
            Expiry = expiry;
        }
        public override string ToString()
        {
            return $"MortgageInsuranceRates [Id={Id}, Effective={Effective.ToString("yyyy-MM-dd")}, Expiry={Expiry.ToString("yyyy-MM-dd")}]";
        }
    }
    
}
