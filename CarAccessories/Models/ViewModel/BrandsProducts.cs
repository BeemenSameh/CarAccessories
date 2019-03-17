using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAccessories.Models.ViewModel
{
    public class BrandsProducts
    {
        public List<Brand> Brands { get; set; }
        public List<VendorProduct> VendorProducts { get; set; }
        public List<Category> CategoriesList { get; set; }
    }
}