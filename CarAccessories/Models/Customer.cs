using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Customer
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [RegularExpression(".*[0-9]", ErrorMessage = "PhoneNumber must be numeric")]
        [StringLength(11)]
        [Required]
        public string PhoneNumber { get; set; }
        [StringLength(14)]
        [RegularExpression(".*[0-9]", ErrorMessage = "NationalID must be numeric")]
        [Required]
        public string NationalID { get; set; }
        public string Photo { get; set; }
        [Required]
        public int money { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
