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
        [Required]
        public string ComponyName { get; set; }
        [Required]
        public string Address { get; set; }
        public string Photo { get; set; }
        public int Accept { get; set; }
        [Required]
        [StringLength(14)]
        public string PhoneNumber { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<VendorProduct> VendorProduct { get; set; }
    }
}
