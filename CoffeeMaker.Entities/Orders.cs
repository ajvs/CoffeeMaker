using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker.Entities
{
    public class Orders
    {
        public Guid Id { get; set; }
        
        [Required]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Drink Ordered")]
        public Guid DrinkOrderedGuid { get; set; }

        public string DrinkOrdered { get; set; }

        [Required]
        public int Quantity { get; set; }

        [DisplayName("Date Ordered")]
        public DateTime DateOrdered { get; set; }

    }
    
}
