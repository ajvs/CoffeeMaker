using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker.Entities
{
    public class Stocks
    {
        public Guid Id { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
