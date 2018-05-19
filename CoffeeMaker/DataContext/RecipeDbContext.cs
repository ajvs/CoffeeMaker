using CoffeeMaker.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeeMaker.DataContext
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Recipe> Recipes { get; set; }

        public System.Data.Entity.DbSet<CoffeeMaker.Entities.Drinks> Drinks { get; set; }

        public System.Data.Entity.DbSet<CoffeeMaker.Entities.Stocks> Stocks { get; set; }
    }
}