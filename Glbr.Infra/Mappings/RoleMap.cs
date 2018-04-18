using Glbr.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Glbr.Infra.Mappings
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Roles");
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(60).IsRequired();

        }
    }
}
