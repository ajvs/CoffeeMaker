using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker.Entities
{
    public class Recipe
    {
        [Key]
        public Guid RecipeId { get; set; }
        
        public Guid DrinkId { get; set; }

        [ForeignKey("DrinkId")]
        public Drinks Drinks { get; set; }

        public Guid StockId { get; set; }

        [ForeignKey("StockId")]
        public Stocks Stocks { get; set; }

        public int Quantity { get; set; }
    }
}
