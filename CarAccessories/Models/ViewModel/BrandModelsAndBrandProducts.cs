using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models.ViewModel
{
    public class BrandModelsAndBrandProducts
    {
        public List<Model> ModelsList { get; set; }
        public List<VendorProduct> VendorProductList { get; set; }
        public List<Category> CategoriesList { get; set; }
        public int BrandId { get; set; }
    }
}