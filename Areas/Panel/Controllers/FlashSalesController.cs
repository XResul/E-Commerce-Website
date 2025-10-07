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
    public class FlashSalesController : BaseBackEndController
    {
        EntityModelContext db = new EntityModelContext();
        // GET: Panel/FlashSales
        public ActionResult Index()
        {
            return View(db.FlashSales.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(flashSales flashSales, HttpPostedFileBase image)
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

                    flashSales.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                }
                db.FlashSales.Add(flashSales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flashSales);
        }




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            flashSales flashSales = db.FlashSales.Find(id);
            if (flashSales == null)
            {
                return HttpNotFound();
            }
            return View(flashSales);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(flashSales flashSales, HttpPostedFileBase image)
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
                    ir.saveJpeg(Server.MapPath("/Uploads/thumb/" + gd.ToString() + uzanti), images[1], 100);


                    flashSales.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                    flashSales.ThumbURL = "/Uploads/thumb/" + gd.ToString() + uzanti;
                }
                db.Entry(flashSales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flashSales);
        }




        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            flashSales flash = db.FlashSales.Find(id);
            if (flash == null)
            {
                return HttpNotFound();
            }
            return View(flash);
        }

        public ActionResult Delete(int id)
        {
            db.FlashSales.Remove(db.FlashSales.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}