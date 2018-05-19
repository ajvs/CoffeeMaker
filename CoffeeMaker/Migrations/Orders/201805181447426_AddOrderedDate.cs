namespace CoffeeMaker.Migrations.Orders
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateOrdered", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateOrdered");
        }
    }
}
