namespace CoffeeMaker.Migrations.Orders
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToDrinkOrdered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DrinkOrdered", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "DrinkOrdered");
        }
    }
}
