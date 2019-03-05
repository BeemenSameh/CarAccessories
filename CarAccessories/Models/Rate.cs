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
        [ForeignKey("Seller")]
        [Index("IX_UserSeller", 1, IsUnique = true)]
        public string Seller_ID { get; set; }
        [ForeignKey("Customer")]
        [Index("IX_UserProduct", 2, IsUnique = true)]
        public string Customer_ID { get; set; }
        public int RateNumber { get; set; }

        [InverseProperty("SellerRate")]
        public ApplicationUser Seller { get; set; }
        [InverseProperty("CustomerRate")]
        public ApplicationUser Customer { get; set; }
    }
}
