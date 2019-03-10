using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Rate
    {
        public int ID { set; get; }
        [ForeignKey("Vendor")]
        [Index("IX_UserVendor", 1, IsUnique = true)]
        public string Vendor_ID { get; set; }
        [ForeignKey("Customer")]
        [Index("IX_UserVendor", 2, IsUnique = true)]
        public string Customer_ID { get; set; }
        public int RateNumber { get; set; }

        public VendorProduct VendorProduct { get; set; }
        public Customer Customer { get; set; }
    }
}
