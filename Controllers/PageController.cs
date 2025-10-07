using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class PageController : Controller
    {
        EntityModelContext db = new EntityModelContext();
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageDetail(int id)
        {

            PageViewModel model = new PageViewModel();
            model.Page_ = db.Pages.Where(p => p.PageID == id && p.IsActive == true).FirstOrDefault();
            return View(model);

        }


    }
}