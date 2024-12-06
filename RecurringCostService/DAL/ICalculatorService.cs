using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCostService.DAL
{
    public interface ICalculatorService
    {
        double CalculateMonthlyPayment(string bankName, double loanAmount, int months);
        double CalculateTotalAmountOfInterest(string bankName, double loanAmount, int months);
    }
}
