using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haliWebProje.Models
{
    public class ContactViewModel
    {
        public Contact contact_ { get; set; }
        public Social social_ { get; set; }

        public IEnumerable<Adress> Adresses__ { get; set; }

    }
}