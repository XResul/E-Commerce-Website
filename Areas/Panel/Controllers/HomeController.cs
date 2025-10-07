using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Areas.Panel.Controllers
{
    public class HomeController : BaseBackEndController
    {
        EntityModelContext db=new EntityModelContext(); 
        // GET: Panel/Home
        public ActionResult Index()
        {
            return View();
        }






    }
}