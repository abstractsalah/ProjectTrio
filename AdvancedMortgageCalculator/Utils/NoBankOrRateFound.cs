using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.Utils
{
    public class NoBankOrRateFound : Exception
    {
        public NoBankOrRateFound(string message) : base(message)

        { }
    }
}
