using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        EntityModelContext db = new EntityModelContext();
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ProductDetail(int id)
        {
            ProductViewModel model = new ProductViewModel();
            model.product_ = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            return View(model);

        }

    }
}