using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haliWebProje.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        
        
        public IEnumerable<bestSellingProducts>  bestSellingProducts { get; set; }
        public IEnumerable<bestSellingProductsCategory>   bestSellingProductsCategories { get; set; }


        public IEnumerable<flashSales>  flashSales { get; set; }


        public myAbout myAbout_ { get; set; }

        public IEnumerable<followOurInstagram> followOurInstagrams { get; set; }

    }
}