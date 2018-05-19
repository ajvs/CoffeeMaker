namespace CoffeeMaker.Migrations.Orders
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDrinkOrderedFK : DbMigration
    {
        public override void Up()
        {   
            AlterColumn("dbo.Orders", "DrinkOrdered", c => c.Guid(nullable: false));
            CreateIndex("dbo.Orders", "DrinkOrdered");
            AddForeignKey("dbo.Orders", "DrinkOrdered", "dbo.Drinks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DrinkOrdered", "dbo.Drinks");
            DropIndex("dbo.Orders", new[] { "DrinkOrdered" });
            AlterColumn("dbo.Orders", "DrinkOrdered", c => c.String(nullable: false));
            DropTable("dbo.Drinks");
        }
    }
}
