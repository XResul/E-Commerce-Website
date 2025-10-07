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
    public class PagesController : BaseBackEndController
    {
        // GET: Panel/Pages
        EntityModelContext db = new EntityModelContext();
        public ActionResult Index()
        {

            return View(db.Pages.ToList());

        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Page page, HttpPostedFileBase image)
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

                    page.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                    page.ThumbURL = "/Uploads/thumb/" + gd.ToString() + uzanti;
                }
                db.Pages.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page);
        }







        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Page page, HttpPostedFileBase image)
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


                    page.ImageURL = "/Uploads/image/" + gd.ToString() + uzanti;
                    page.ThumbURL = "/Uploads/thumb/" + gd.ToString() + uzanti;
                }
                db.Entry(page).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page);

        }


        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);

        }


        public ActionResult pasifYap(int id)
        {
            Page page = db.Pages.Find(id);
            page.IsActive = false;
            db.Entry(page).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult aktifYap(int id)
        {
            Page page = db.Pages.Find(id);
            page.IsActive = true;
            db.Entry(page).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }





    }
}