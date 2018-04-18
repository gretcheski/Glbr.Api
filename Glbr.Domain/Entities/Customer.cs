using System;

namespace Glbr.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CPF { get; set; }

        public string Address { get; set; }

    }
}
