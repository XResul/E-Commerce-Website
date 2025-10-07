using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haliWebProje.Models
{
    public class ProductCategoryViewModel
    {
        public IEnumerable<Product> products__ { get; set; }  // Ürünlerin listesi

        //public ProductCategory category_ { get; set; }



        //public int CurrentPage { get; set; }         // Şu anki sayfa numarası
        //public int TotalPages { get; set; }          // Toplam sayfa sayısı
        //public bool HasPreviousPage { get; set; }    // Önceki sayfa var mı?
        //public bool HasNextPage { get; set; }        // Sonraki sayfa var mı?
    }
}