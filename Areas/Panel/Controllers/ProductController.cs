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
    public class ProductController : BaseBackEndController
    {
        EntityModelContext db = new EntityModelContext();
        // GET: Panel/Product
        public ActionResult Index()
        {
            var product = db.Products.Include(p => p.ProductCategory);
            return View(product.ToList());
        }


        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.productCategory, "ProductCategoryID", "ProductCategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase image)
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


                    product.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                    product.ThumbURL = "/Uploads/thumb/" + gd.ToString() + uzanti;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.productCategory, "ProductCategoryID", "ProductCategoryName", product.ProductCategoryID);



            return View(product);

        }





        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            Product project = db.Products.Find(id);

            ViewBag.ProductCategoryId = new SelectList(db.productCategory, "ProductCategoryID", "ProductCategoryName");

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
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


                    product.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.productCategory, "ProductCategoryID", "ProductCategoryName", product.ProductCategoryID);

            return View(product);

        }


        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductCategoryId = new SelectList(db.productCategory, "ProductCategoryID", "ProductCategoryName");
            return View(product);
        }

        public ActionResult Delete(int id)
        {

            db.Products.Remove(db.Products.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}