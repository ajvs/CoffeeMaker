using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker.Entities
{
    public class Drinks
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Drink Name")]
        public string DrinkName { get; set; }
    }
}
