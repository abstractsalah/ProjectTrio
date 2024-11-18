using AdvancedMortgageCalculator.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.DAL
{
    public interface IBankDAO
    {
        IList<Bank> GetAllBanks();

    }
}
