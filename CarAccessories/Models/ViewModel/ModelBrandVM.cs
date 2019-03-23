using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models.ViewModel
{
    public class ModelBrandVM
    {
        public int ID { get; set; }
        public string ModelName { get; set; }
        public string Year { get; set; }
        public int Brandid { get; set; }

        public Brand Brand { get; set; }
        public List<Brand> Brands { get; set; }

    }
}