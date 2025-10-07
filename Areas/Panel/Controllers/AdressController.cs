using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Areas.Panel.Controllers
{
    public class AdressController : BaseBackEndController
    {
        EntityModelContext db = new EntityModelContext();
        // GET: Panel/Adress
        public ActionResult Index()
        {
            return View(db.Adresses.ToList());
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adress adress)
        {
            if (ModelState.IsValid)
            {
                db.Adresses.Add(adress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adress);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Adress adress = db.Adresses.Find(id);
            if (adress == null)
            {
                return HttpNotFound();
            }
            return View(adress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adress adress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adress).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adress);
        }


        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Adress adress = db.Adresses.Find(id);
            if (adress == null)
            {
                return HttpNotFound();
            }
            return View(adress);
        }


        public ActionResult Delete(int id)
        {

            db.Adresses.Remove(db.Adresses.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}