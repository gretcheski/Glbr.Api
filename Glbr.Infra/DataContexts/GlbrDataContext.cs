using MySql.Data.Entity;
using System.Data.Entity;
using Glbr.Domain.Entities;
using Glbr.Infra.Mappings;

namespace Glbr.Infra.DataContexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GlbrDataContext : DbContext
    {
        public GlbrDataContext() : base("server=localhost;port=3306;database=glbr;uid=root;password=glbrCanecas@00")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<GlbrDataContext>(new GlbrDataContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new SaleMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }

    public class GlbrDataContextInitializer : DropCreateDatabaseAlways<GlbrDataContext>
    {
        protected override void Seed(GlbrDataContext context)
        {

            context.Roles.Add(new Role { Id = 1, Title = "Usuário" });
            context.Roles.Add(new Role { Id = 2, Title = "Administrador" });
            context.SaveChanges();

            context.Sales.Add(new Sale { Amount = 20, ValuePerProduct = 10.50, TotalValue = 2000.99});
            context.Sales.Add(new Sale { Amount = 13, ValuePerProduct = 12.77, TotalValue = 2475.99 });
            context.Sales.Add(new Sale { Amount = 20, ValuePerProduct = 130.33, TotalValue = 33807.99 });
            context.SaveChanges();

            context.Customers.Add(new Customer {Address = "Rua Antonio Dariva, 206", CPF = "144078907744", Name = "Gabriel Sturm"});
            context.Customers.Add(new Customer {Address = "Rua Walenty Golas, 370", CPF = "03573273244", Name = "Lucas Lima" });
            context.Customers.Add(new Customer {Address = "Rua Antonio Dariva, 304", CPF = "09977001144", Name = "Ricardo Duarte" });
            context.Customers.Add(new Customer {Address = "Praça Osório", CPF = "99971173744", Name = "Ciclano Teste"});
            context.Customers.Add(new Customer {Address = "Avenida Sete de Setembro, 2400", CPF = "06475555744", Name = "Bruno Falarz"});
            context.Customers.Add(new Customer {Address = "Gastão Camara, 500", CPF = "06477777744", Name = "Gabriel Retcheski" });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
