using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication2.Models.Entity;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        FGDBEntities1 FGDB = new FGDBEntities1();
        public ActionResult Index()
        {
            var context = FGDB.ProductTBL.ToList();
            var discountContext = FGDB.DiscountTBL.ToList();
            List<ProductTBL> updatedProducts = new List<ProductTBL>();

            foreach (var product in context)
            {
                foreach (var discount in discountContext)
                {

                    DateTime currentDate = DateTime.Now;
                    if (product.product_category == discount.discountedproducts && currentDate > discount.startdate && currentDate < discount.enddate)
                    {
                        decimal newPrice = (decimal)(product.product_price - (product.product_price * (discount.discountamount / Convert.ToDecimal(100.00))));
                        product.product_price = newPrice;
                        updatedProducts.Add(product);
                    }
                }
            }


            return View(context);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CampaignUpdateinfo(int id)
        {
            var urun = FGDB.DiscountTBL.Find(id);

            return View("CampaignUpdateinfo", urun);
        }
        [HttpGet]
        public ActionResult ADDProduct()
        {

            return View();

        }
        [HttpPost]
        public ActionResult ADDProduct(ProductTBL p1)
        {
            if (ModelState.IsValid)
            {
                FGDB.ProductTBL.Add(p1);

                FGDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(p1);
            }
        }


        public ActionResult RemoveProduct(int id)
        {
            var product = FGDB.ProductTBL.Find(id);
            FGDB.ProductTBL.Remove(product);
            FGDB.SaveChanges();
            return RedirectToAction("Index");

        }
    }


}