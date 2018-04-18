namespace Glbr.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        CPF = c.String(nullable: false, unicode: false),
                        Address = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false, precision: 0),
                        Amount = c.Int(nullable: false),
                        ValuePerProduct = c.Double(nullable: false),
                        TotalValue = c.Double(nullable: false),
                        Customer_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Password = c.String(maxLength: 32, fixedLength: true, storeType: "nchar"),
                        Customer_Id = c.Guid(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Customer_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Email, unique: true, name: "IX_EMAIL")
                .Index(t => t.Customer_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.Sales", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "Customer_Id" });
            DropIndex("dbo.Users", "IX_EMAIL");
            DropIndex("dbo.Sales", new[] { "Customer_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.Roles");
            DropTable("dbo.Customer");
        }
    }
}
