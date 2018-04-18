using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glbr.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string ProductType { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public float Weight { get; set; }

        public double Price { get; set; }

        public double Cost { get; set; }
    }
}
