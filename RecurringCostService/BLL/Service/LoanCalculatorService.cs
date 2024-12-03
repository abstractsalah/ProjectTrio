using AdvancedMortgageCalculator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCostService.BLL.Service
{
    public class LoanCalculatorService

    {
        public Online_DAO onlineDao;

        // Injecter le DAO dans le constructeur
        public LoanCalculatorService(Online_DAO onlineDao)
        {
            this.onlineDao = onlineDao;
        }

        public double CalculateMonthlyPayment(String bankName, double loanAmount, int months)
        {
            double P = loanAmount;
            int n = months;

            // calculate monthly interest rate
            var interestRates= this.onlineDao.FetchBankInterestRateByName(bankName);
         
            // Utiliser le taux d'intérêt le plus bas
            double annualInterestRate = interestRates.MortgageInterestRates.Min(rate => rate.Rate);
            double r = annualInterestRate / 100 / 12;
            // calculate monthly insurance rate
            var insuranceRates = this.onlineDao.FetchMortgageInsuranceRateByBank(bankName);

            // Utiliser le taux d'assurance le plus bas
            double annualInsuranceRate = insuranceRates.Min(rate => rate.Rate);
            double a = annualInsuranceRate / 100 / 12;
            //// Monthly payment formula
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
