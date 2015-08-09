namespace AnotherShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartModels",
                c => new
                    {
                        CartId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CartModels");
        }
    }
}
