using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class HomeController : Controller
    {
        EntityModelContext db = new EntityModelContext();

        public ActionResult Index()
        {

            HomeViewModel model = new HomeViewModel();
            model.Sliders = db.Sliders.ToList();


            model.bestSellingProducts = db.bestSellingProducts.ToList();
            model.bestSellingProductsCategories = db.bestSellingProductsCategory.Where(p => p.IsActive == true).ToList();



            var categories = db.bestSellingProductsCategory.Include(c => c.bestSellingProducts).ToList(); // Kategoriler ve ürünler
            var products = db.bestSellingProducts.ToList(); // Tüm ürünler

            var viewModel = new HomeViewModel
            {
                bestSellingProductsCategories = categories,
                bestSellingProducts = products
            };

            model.flashSales = db.FlashSales.ToList();

            model.myAbout_ = db.myAbouts.Take(1).FirstOrDefault();

            model.followOurInstagrams = db.followOurInstagrams.ToList();

            return View(model);
        }


        // En çok satanlar sayfası için kategori ve ürünleri alıyoruz
        public ActionResult BestSellingProductPartial()
        {
            var model = new HomeViewModel
            {
                bestSellingProductsCategories = db.bestSellingProductsCategory
                                                         .Where(c => c.IsActive)
                                                         .Include(c => c.bestSellingProducts) // Kategorilere ait ürünleri dahil ediyoruz
                                                         .ToList(),
                bestSellingProducts = db.bestSellingProducts
                                              .Where(p => p.bestSellingProductsCategory.IsActive) // Aktif olan ürünleri çekiyoruz
                                              .ToList()
            };



            return PartialView("BestSellingProductPartial", model);
        }




        //// AJAX ile kategoriye özel ürünleri alıyoruz
        //[HttpPost]
        //public ActionResult BestSellingProductPartial(int categoryId)
        //{
        //    HomeViewModel model = new HomeViewModel();
        //    model.bestSellingProducts = db.bestSellingProducts.Where(p => p.bestSellingProductsCategoryID == categoryId).ToList();
        //    model.bestSellingProductsCategories = db.bestSellingProductsCategory.Where(c => c.IsActive).ToList();

        //    return PartialView("BestSellingProductPartial", model);

        //}

        //jquery ve javascript ile yapcagız sorunun cözümünü


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult flashSalesPartial()
        {
            HomeViewModel model = new HomeViewModel();
            model.flashSales = db.FlashSales.ToList();

            return PartialView("flashSalesPartial", model);

        }




        public ActionResult myAboutPartial()
        {
            HomeViewModel model = new HomeViewModel();
            model.myAbout_ = db.myAbouts.Take(1).FirstOrDefault();


            return PartialView("myAboutPartial", model);

        }

        public ActionResult followInstagramPartial()
        {
            HomeViewModel model = new HomeViewModel();
            model.followOurInstagrams = db.followOurInstagrams.ToList();


            return PartialView("followInstagramPartial", model);

        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SliderPartial()
        {
            HomeViewModel model = new HomeViewModel();
            model.Sliders = db.Sliders.ToList();
            return PartialView("SliderPartial", model);
        }





    }
}