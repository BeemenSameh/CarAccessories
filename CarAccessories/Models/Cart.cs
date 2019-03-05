using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}
