using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Areas.Panel.Controllers
{
    public class UsersController : BaseBackEndController
    {
        EntityModelContext db = new EntityModelContext();
        // GET: Panel/Users
        public ActionResult Index()
        {
            return View(db.Users.Where(p => p.UserID > 1).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                string passwordHash = WebLibrary.CryptoClass.ToSHA1Hash(users.UserPassword);
                users.UserPassword = passwordHash;

                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {
                string sifreHash = WebLibrary.CryptoClass.ToSHA1Hash(users.UserPassword);

                users.UserPassword = sifreHash;

                db.Entry(users).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }


        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }



        public ActionResult Delete(int id)
        {
            db.Users.Remove(db.Users.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}