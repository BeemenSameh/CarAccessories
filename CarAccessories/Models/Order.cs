using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
        public bool IsBuy { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public Customer Customer { get; set; }
    }
}
