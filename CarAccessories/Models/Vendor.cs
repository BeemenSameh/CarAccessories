using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Vendor
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string ID { get; set; }
        public string ComponyName { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public int Accept { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<VendorProduct> VendorProduct { get; set; }
    }
}
