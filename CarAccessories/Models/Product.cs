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
        public int Price { get; set; }
        public string Image { get; set; }
        public string MinDescription { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string State { get; set; }

        public DateTime Insert_Date { get; set; } = DateTime.Now;
        public double? Sale_price { get; set; }

        public Model Model { get; set; }
        public virtual ICollection<VendorProduct> VendorProducts { get; set; }
    }
}
