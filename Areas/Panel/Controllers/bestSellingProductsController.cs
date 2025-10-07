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
    public class bestSellingProductsController : BaseBackEndController
    {


        // GET: Panel/bestSellingProducts
        EntityModelContext db = new EntityModelContext();

        public ActionResult Index()
        {
            var bestSellingProducts = db.bestSellingProducts.Include(p => p.bestSellingProductsCategory).ToList();
            return View(bestSellingProducts);
        }
        public ActionResult Create()
        {
            ViewBag.bestSellingProductsCategoryID = new SelectList(db.bestSellingProductsCategory, "bestSellingProductsCategoryID", "bestSellingProductsCategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(bestSellingProducts bestSellingProducts, HttpPostedFileBase image)
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


                    bestSellingProducts.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                    bestSellingProducts.ThumbURL = "/Uploads/thumb/" + gd.ToString() + uzanti;
                }
                db.bestSellingProducts.Add(bestSellingProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bestSellingProductsCategoryID = new SelectList(db.bestSellingProducts, "bestSellingProductsCategoryID", "bestSellingProductsCategoryName", bestSellingProducts.bestSellingProductsCategoryID);
            return View(bestSellingProducts);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            bestSellingProducts bestSellingProducts = db.bestSellingProducts.Find(id);
            ViewBag.bestSellingProductsCategoryID = new SelectList(db.bestSellingProductsCategory, "bestSellingProductsCategoryID", "bestSellingProductsCategoryName");
            if (bestSellingProducts == null)
            {
                return HttpNotFound();
            }
            return View(bestSellingProducts);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(bestSellingProducts bestSellingProducts, HttpPostedFileBase image)
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


                    bestSellingProducts.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                    bestSellingProducts.ThumbURL = "/Uploads/thumb/" + gd.ToString() + uzanti;
                }
                db.Entry(bestSellingProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bestSellingProductsCategoryID = new SelectList(db.bestSellingProductsCategory, "bestSellingProductsCategoryID", "bestSellingProductsCategoryName");
            return View(bestSellingProducts);
        }


        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            bestSellingProducts bestSellingProducts = db.bestSellingProducts.Find(id);
            if (bestSellingProducts == null)
            {
                return HttpNotFound();
            }
            return View(bestSellingProducts);

        }


        public ActionResult Delete(int id)
        {

            db.bestSellingProducts.Remove(db.bestSellingProducts.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}