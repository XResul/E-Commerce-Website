using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Areas.Panel.Controllers
{
    public class followOurInstagramsController : BaseBackEndController
    {
        // GET: Panel/followOurInstagrams
        EntityModelContext db = new EntityModelContext();
        public ActionResult Index()
        {
            return View(db.followOurInstagrams.ToList());
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(followOurInstagram followOurInstagram, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    Image img = Image.FromStream(image.InputStream);

                    Guid gd = Guid.NewGuid();

                    string uzanti = Path.GetExtension(image.FileName);

                    WebLibrary.GraphicClass.ImageResizer ir = new WebLibrary.GraphicClass.ImageResizer();
                    List<Image> lstimage = ir.Resize(img, 800, 350);

                    ir.saveJpeg(Server.MapPath("/Uploads/image/" + gd.ToString() + uzanti), lstimage[0], 100);

                    followOurInstagram.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                }
                db.followOurInstagrams.Add(followOurInstagram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(followOurInstagram);
        }




        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            followOurInstagram follow = db.followOurInstagrams.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(followOurInstagram follow, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    Image img = Image.FromStream(image.InputStream);

                    Guid gd = Guid.NewGuid();

                    string uzanti = Path.GetExtension(image.FileName);

                    WebLibrary.GraphicClass.ImageResizer ir = new WebLibrary.GraphicClass.ImageResizer();
                    List<Image> lstimage = ir.Resize(img, 800, 350);

                    ir.saveJpeg(Server.MapPath("/Uploads/image/" + gd.ToString() + uzanti), lstimage[0], 100);

                    follow.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                }
                db.Entry(follow).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(follow);
        }



        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            followOurInstagram follow = db.followOurInstagrams.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        public ActionResult Delete(int id)
        {
            db.followOurInstagrams.Remove(db.followOurInstagrams.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}