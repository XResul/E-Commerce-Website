using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{

    public class LayoutRepositoryController : Controller
    {
        EntityModelContext db = new EntityModelContext();
        // GET: LayoutRepository
        public ActionResult HeaderPartial()
        {
            HeaderViewModel model = new HeaderViewModel();
            model.categories = db.productCategory.Where(p => p.IsActive == true).ToList();

            model.Pages = db.Pages.Where(p => p.IsActive == true).ToList();

            model.social_ = db.Socials.Take(1).ToList().FirstOrDefault();


            model.Contact_ = db.Contacts.Take(1).ToList().FirstOrDefault();

            return PartialView("HeaderComponentPartial", model);
        }

        public ActionResult HeadParital()
        {
            return PartialView("HeadComponentPartial");
        }


        public ActionResult FooterPartial()
        {

            FooterViewModel model = new FooterViewModel();
            model.categories__ = db.productCategory.Where(p => p.IsActive == true).ToList();
            model.contact_ = db.Contacts.Take(1).FirstOrDefault();
            model.social_ = db.Socials.Take(1).FirstOrDefault();
            model.Pages__ = db.Pages.Where(p => p.IsActive == true).ToList();
            model.Adresses__ = db.Adresses.ToList();


            return PartialView("FooterPartialComponent", model);
        }



        public ActionResult ScriptPartial()
        {
            return PartialView("ScriptPartialComponent");
        }


    }
}