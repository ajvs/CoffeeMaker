using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CoffeeMaker.Entities;

namespace CoffeeMaker.DataContext
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Orders> Orders { get; set; }
    }
}