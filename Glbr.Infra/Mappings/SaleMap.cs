using Glbr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glbr.Infra.Mappings
{
    public class SaleMap : EntityTypeConfiguration<Sale>
    {
        public SaleMap()
        {
            ToTable("Sales");

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.SaleDate).IsRequired();
            Property(x => x.Amount).IsRequired();
            Property(x => x.ValuePerProduct).IsRequired();
            Property(x => x.TotalValue).IsRequired();

        }
    }
}
