namespace CoffeeMaker.Migrations.Recipe
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRecipe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Guid(nullable: false),
                        DrinkId = c.Guid(nullable: false),
                        StockId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Drinks", t => t.DrinkId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.DrinkId)
                .Index(t => t.StockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Recipes", "DrinkId", "dbo.Drinks");
            DropIndex("dbo.Recipes", new[] { "StockId" });
            DropIndex("dbo.Recipes", new[] { "DrinkId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Drinks");
            DropTable("dbo.Recipes");
        }
    }
}
