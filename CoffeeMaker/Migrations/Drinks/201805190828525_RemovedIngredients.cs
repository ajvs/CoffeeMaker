namespace CoffeeMaker.Migrations.Drinks
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIngredients : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Drinks", "CoffeeBeans");
            DropColumn("dbo.Drinks", "Sugar");
            DropColumn("dbo.Drinks", "Milk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drinks", "Milk", c => c.Int(nullable: false));
            AddColumn("dbo.Drinks", "Sugar", c => c.Int(nullable: false));
            AddColumn("dbo.Drinks", "CoffeeBeans", c => c.Int(nullable: false));
        }
    }
}
