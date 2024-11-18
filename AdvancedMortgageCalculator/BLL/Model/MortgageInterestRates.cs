using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.BLL.Model
{
    public class MortgageInterestRates
    {
        public static int _AUTO_GEN = 1;

        public int Id { get; set; }
        public double Rate { get; set; }

        public DateTime Effective {  get; set; }

        public DateTime Expiry { get; set; }

        public MortgageInterestRates() 
        {
            this.Id = MortgageInsuranceRates._AUTO_GEN++;
        }

        public MortgageInterestRates(int id, double rate , DateTime effective , DateTime expiry) 
        {
            this.Id = id;
            this.Rate = rate;
            this.Effective = effective;
            this.Expiry = expiry;

        }
        public override string ToString()
        {
            return $"MortgageInterestRates [Id={Id}, Rate={Rate:F2}%, Effective={Effective:yyyy-MM-dd}, Expiry={Expiry:yyyy-MM-dd}]";
        }
    }
}
