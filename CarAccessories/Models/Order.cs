using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int TotalPrice { get; set; }
        public bool Isbuy { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public Customer Customer { get; set; }
    }
}
