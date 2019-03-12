using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string MinDescription { get; set; }
        public string State { get; set; }

        public Model Model { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<VendorProduct> VendorProducts { get; set; }
    }
}
