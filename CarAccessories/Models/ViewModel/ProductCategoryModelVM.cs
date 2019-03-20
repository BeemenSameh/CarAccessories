using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models.ViewModel
{
    public class ProductCategoryModelVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string MinDescription { get; set; }
        public string State { get; set; }
        public Model Model { get; set; }
        public Category Category { get; set; }

        public List<Model> Models { get; set; }
        public List<Category> Categories { get; set; }
    }
}