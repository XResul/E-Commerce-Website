using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using haliEntityLayer.Entity;


namespace haliWebProje.Areas.Panel.Controllers
{
    public class bestSellingProductsCategoryController : BaseBackEndController
    {
        // GET: Panel/bestSellingProductsCategory
        EntityModelContext db = new EntityModelContext();
        public ActionResult Index()
        {
            return View(db.bestSellingProductsCategory.ToList());
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(bestSellingProductsCategory category, HttpPostedFileBase image)
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

                    category.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;

                }
                db.bestSellingProductsCategory.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }



        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            bestSellingProductsCategory category = db.bestSellingProductsCategory.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(bestSellingProductsCategory category, HttpPostedFileBase image)
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

                    category.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;

                }
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }



        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            bestSellingProductsCategory category = db.bestSellingProductsCategory.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);

        }



        public ActionResult pasifYap(int id)
        {
            bestSellingProductsCategory category = db.bestSellingProductsCategory.Find(id);
            category.IsActive = false;
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult aktifYap(int id)
        {
            bestSellingProductsCategory category = db.bestSellingProductsCategory.Find(id);
            category.IsActive = true;
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}