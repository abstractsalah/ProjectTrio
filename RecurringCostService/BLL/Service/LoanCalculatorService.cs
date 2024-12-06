using AdvancedMortgageCalculator.DAL;
using RecurringCostService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCostService.BLL.Service
{
    public class LoanCalculatorService : ICalculatorService
    {
        private readonly IOnlineDAO onlineDao;

        public LoanCalculatorService(IOnlineDAO onlineDao)
        {
            this.onlineDao = onlineDao;
        }

        public double CalculateMonthlyPayment(String bankName, double loanAmount, int months)
        {
            double P = loanAmount;
            int n = months;

            var interestRates= this.onlineDao.FetchBankInterestRateByName(bankName);
            double annualInterestRate = interestRates.MortgageInterestRates.Min(rate => rate.Rate);
            double r = annualInterestRate / 100 / 12;
            var insuranceRates = this.onlineDao.FetchMortgageInsuranceRateByBank(bankName);

            double annualInsuranceRate = insuranceRates.Min(rate => rate.Rate);
            double a = annualInsuranceRate / 100 / 12;
            //// formula of monthly payment 
            double combinedRate = r + a;
            double numerator = P * combinedRate * Math.Pow(1 + combinedRate, n);
            double denominator = Math.Pow(1 + combinedRate, n) - 1;
            double M = numerator / denominator;

            return M;
        }

        public double CalculateTotalAmountOfInterest(String bankName, double loanAmount, int months)
        {
            double P = loanAmount;
            int n = months;

            double M = CalculateMonthlyPayment(bankName, loanAmount, months);
            double I = (M * n) - P;
            return I;
        }
    }
}
