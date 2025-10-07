using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haliWebProje.Models
{
    public class HeaderViewModel
    {

        public Social social_ { get; set; }

        public Contact Contact_ { get; set; }


        public IEnumerable<Page> Pages { get; set; }


        public IEnumerable<ProductCategory> categories { get; set; }

    }

}