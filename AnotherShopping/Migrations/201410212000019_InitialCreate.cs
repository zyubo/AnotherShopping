namespace AnotherShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(nullable: false),
                        ProductImage = c.String(),
                        ProductPrice = c.Single(nullable: false),
                        ProductAvailable = c.Int(nullable: false),
                        ProductDetails = c.String(),
                        CreatedTime = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductModels");
        }
    }
}
