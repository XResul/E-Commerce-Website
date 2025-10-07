using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class bestSellingProductController : Controller
    {
        // GET: bestSellingProduct
        EntityModelContext db=new EntityModelContext(); 
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult bestSellingProductDetail(int id)
        {

            bestSellingViewModel model = new bestSellingViewModel(); 
            
            bestSellingProducts bestSellingProducts=db.bestSellingProducts.Find(id);

            model.BestSellingProducts_ = bestSellingProducts;

            return View(model); 
        }



    }
}