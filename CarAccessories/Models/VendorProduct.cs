using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class VendorProduct
    {
        public int ID { get; set; }

        public Vendor Vendor { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<Description> Description { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}