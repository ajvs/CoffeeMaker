namespace CoffeeMaker.Migrations.Orders
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "DrinkOrdered", "dbo.Drinks");
            DropIndex("dbo.Orders", new[] { "DrinkOrdered" });
            DropTable("dbo.Drinks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DrinkName = c.String(nullable: false),
                        CoffeeBeans = c.Int(nullable: false),
                        Sugar = c.Int(nullable: false),
                        Milk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "DrinkOrdered");
            AddForeignKey("dbo.Orders", "DrinkOrdered", "dbo.Drinks", "Id", cascadeDelete: true);
        }
    }
}
