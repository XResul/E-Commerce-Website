using haliEntityLayer.Entity;
using haliWebProje.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class ProductCategoryController : Controller
    {
        EntityModelContext db = new EntityModelContext();
        // GET: ProductCategory
        public ActionResult Index()
        {

            return View();

        }

        public ActionResult categoryDetail(int id)
        {
            ProductCategoryViewModel model = new ProductCategoryViewModel();

            model.products__ = db.Products.Where(p => p.ProductCategoryID == id).ToList();






            var productsJson = JsonConvert.SerializeObject(model.products__);
            ViewBag.ProductsJson = productsJson;


 

            return View(model);
        }


    }
}

