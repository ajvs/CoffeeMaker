namespace CoffeeMaker.Migrations.Drinks
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDrinks : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Drinks");
        }
    }
}
