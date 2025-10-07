using haliEntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Areas.Panel.Controllers
{
    public class myAboutController : BaseBackEndController
    {
        EntityModelContext db = new EntityModelContext();

        // GET: Panel/myAbout
        public ActionResult Index()
        {
            return View(db.myAbouts.ToList());
        }



        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(myAbout myAbout, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    WebLibrary.GraphicClass.ImageResizer ir = new WebLibrary.GraphicClass.ImageResizer();
                    Image img = Image.FromStream(image.InputStream);
                    string uzanti = Path.GetExtension(image.FileName);
                    Guid gd = Guid.NewGuid();

                    List<Image> images = ir.Resize(img, 800, 350);

                    ir.saveJpeg(Server.MapPath("/Uploads/image/" + gd.ToString() + uzanti), images[0], 100);


                    myAbout.ImageUrl = "/Uploads/image/" + gd.ToString() + uzanti;
                }
                db.myAbouts.Add(myAbout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(myAbout);

        }


        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            myAbout myAbouts = db.myAbouts.Find(id);
            if (myAbouts == null)
            {
                return HttpNotFound();
            }
            return View(myAbouts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(myAbout myAbout, HttpPostedFileBase image)
        {



            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    WebLibrary.GraphicClass.ImageResizer ir = new WebLibrary.GraphicClass.ImageResizer();
                    Image img = Image.FromStream(image.InputStream);
                    string uzanti = Path.GetExtension(image.FileName);
                    Guid gd = Guid.NewGuid();

                    List<Image> images = ir.Resize(img, 800, 350);

                    ir.saveJpeg(Server.MapPath("/Uploads/image/" + gd.ToString() + uzanti), images[0], 100);


                    myAbout.ImageUrl = "/Uploads/image/" + gd.ToString() + uzanti;
                }
                db.Entry(myAbout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myAbout);

        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            myAbout myAbout = db.myAbouts.Find(id);
            if (myAbout == null)
            {
                return HttpNotFound();
            }
            return View(myAbout);

        }

        public ActionResult Delete(int id)
        {
            db.myAbouts.Remove(db.myAbouts.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}