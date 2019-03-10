using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.ViewModel
{
    public class OrderDetailsViewModel
    {
        //Customer Details
        public string CustomerID { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerNationalID { get; set; }

        //Order 
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderTotalPrice { get; set; }

        //Order Details
        public int OrderDetailsID { get; set; }
        public int OrderDetailsQuantity { get; set; }
        public int OrderDetailsPrice { get; set; }
    }
}