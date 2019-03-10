using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class VendorProduct
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public double? Sale_price { get; set; }
        public DateTime Insert_Date { get; set; } = DateTime.Now;

        public Vendor Vendor { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<Description> Description { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}