using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMortgageCalculator.BLL.Model
{
    public class Product
    {

        public int Id { get; set; }
        public string Type { get; set; }

        public Product(int id, string type)
        {

            this.Id = id;
            this.Type = type;
        }

        public Product() { }

        override
        public string ToString()
        {

            return $"Id = {this.Id} , Type = {this.Type}";
        }

    }
    


}
