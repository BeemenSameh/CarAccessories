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

        public int Price { get; set; }
        public double? Sale_price { get; set; }
        public int Quantity { get; set; }
        public DateTime Insert_Date { get; set; } = DateTime.Now;

        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public List<Model> Models { get; set; }
        public List<Category> Categories { get; set; }
    }
}