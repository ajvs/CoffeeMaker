using CoffeeMaker.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeeMaker.DataContext
{
    public class StocksDbContext : DbContext
    {
        public StocksDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Stocks> Stocks { get; set; }
    }
}