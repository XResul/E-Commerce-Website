using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class flashSalesController : Controller
    {
        // GET: flashSales
        EntityModelContext db = new EntityModelContext();
        public ActionResult index()
        {
            return View();
        }

        public ActionResult flashSalesDetail(int id)
        {
            flashSalesViewModel model = new flashSalesViewModel();
            
            flashSales flash = db.FlashSales.Find(id);
            model.flashSales_=flash;    

            return View(model);
        }
    }
}