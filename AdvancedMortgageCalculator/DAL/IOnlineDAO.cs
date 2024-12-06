using AdvancedMortgageCalculator.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.DAL
{
    public interface IOnlineDAO
    {
        Bank FetchBankInterestRateByName(string bankName);

        Bank GetBankWithLowestInterestRate();

        IList<Bank> FetchBanksByProductType(string productType);

        IList<MortgageInsuranceRates> FetchMortgageInsuranceRateByBank(string bankName);

    }
}
