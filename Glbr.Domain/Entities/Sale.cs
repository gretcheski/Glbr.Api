using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glbr.Domain.Entities
{
    public class Sale
    {

        public Guid Id { get; set; }

        public DateTime SaleDate { get; set; }

        public List<Product> Products { get; set; }

        public int Amount { get; set; }

        public double ValuePerProduct { get; set; }

        public double TotalValue { get; set; }

        public Customer Customer { get; set; }
    }
}
