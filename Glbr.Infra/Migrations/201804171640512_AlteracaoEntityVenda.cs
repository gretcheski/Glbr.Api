namespace Glbr.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoEntityVenda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductType = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Capacity = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        Price = c.Double(nullable: false),
                        Cost = c.Double(nullable: false),
                        Sale_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sales", t => t.Sale_Id)
                .Index(t => t.Sale_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Sale_Id", "dbo.Sales");
            DropIndex("dbo.Products", new[] { "Sale_Id" });
            DropTable("dbo.Products");
        }
    }
}
