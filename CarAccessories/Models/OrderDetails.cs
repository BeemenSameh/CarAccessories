using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public VendorProduct VendorProduct { get; set; }
        public Order Order { get; set; }
    }
}