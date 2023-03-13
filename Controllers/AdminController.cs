using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entity;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        FGDBEntities1 FGDB = new FGDBEntities1();

        // GET: Admin
        public ActionResult Index()
        {
            var context = FGDB.DiscountTBL.ToList();
            return View(context);
        }

        public ActionResult RemoveCampaign(int id)
        {
            var campaign = FGDB.DiscountTBL.Find(id);
            FGDB.DiscountTBL.Remove(campaign);
            FGDB.SaveChanges();
            return RedirectToAction("Index");

        }


        public ActionResult CampaignUpdate(DiscountTBL discount)
        {
            var urun = FGDB.DiscountTBL.Find(discount.ID);
            urun.startdate = discount.startdate;
            urun.enddate = discount.enddate;
            urun.discountedproducts = discount.discountedproducts;
            urun.discountamount = discount.discountamount;
            urun.campaignname = discount.campaignname;
            FGDB.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CampaignUpdateinfo(int id)
        {
            var urun = FGDB.DiscountTBL.Find(id);
            
            return View("CampaignUpdateinfo", urun);
        }
        [HttpGet]
        public ActionResult ADDCampaign()
        {

            return View();

        }
        [HttpPost]
        public ActionResult ADDCampaign(DiscountTBL p1)
        {
            if (ModelState.IsValid)
            {
                FGDB.DiscountTBL.Add(p1);

                FGDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(p1);
            }
        }
    }
}