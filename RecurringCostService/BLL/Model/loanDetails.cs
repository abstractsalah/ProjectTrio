using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCostService.BLL.Model
{
    public class loanDetails
    {
        public double LoanAmount { get; set; }
        public int NumberOfPayments { get; set; }
        public double AnnualInterestRate { get; set; }
        public double AnnualInsuranceRate { get; set; }
    }
}
