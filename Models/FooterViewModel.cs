using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haliWebProje.Models
{
    public class FooterViewModel
    {

        public Social social_ { get; set; }
        public Contact contact_ { get; set; }

        public IEnumerable<ProductCategory> categories__ { get; set; }

        public IEnumerable<Page> Pages__ { get; set; }

        public IEnumerable<Adress> Adresses__ { get; set; }

    }
}