using AdvancedMortgageCalculator.BLL.Model;
using AdvancedMortgageCalculator.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.DAL
{
    public class BankDAO : IBankDAO
    {
       private InMemoryDataStore repository;

        public BankDAO()
        {
            this.repository = InMemoryDataStore.GetInstance();
        }


        public IList<Bank> GetAllBanks()
        {
            return repository.Banks;
        }

    }
}
