using CoffeeMaker.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeeMaker.DataContext
{
    public class DrinksDbContext : DbContext
    {
        public DrinksDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Drinks> Drinks { get; set; }
    }
}