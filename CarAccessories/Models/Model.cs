using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models
{
    public class Model
    {
        public int ID { get; set; }
        public string ModelName { get; set; }
        public string Year { get; set; }

        public Brand Brand { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
