using PrsWebApp.Models;
using PrsWebAppProject.Models;
using PrsWebAppProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace PrsWebAppProject.Controllers
{
    public class VendorsController : Controller
    {
        private PrsDbContext db = new PrsDbContext();

        //List
        public ActionResult List()
        {
            return Json(db.Vendors.ToList(), JsonRequestBehavior.AllowGet);
        }

        //Get
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }
        // Create
        public ActionResult Create([System.Web.Http.FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Vendors.Add(vendor);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Vendor was created,"));
        }

        //Change
        public ActionResult Change([FromBody] Vendor vendor)
        {
            Vendor vendor2 = db.Vendors.Find(vendor.Id);
            vendor2.Code = vendor.Code;
            vendor2.Name = vendor.Name;
            vendor2.Address = vendor.Address;
            vendor2.City = vendor.City;
            vendor2.State = vendor.State;
            vendor2.Zip = vendor.Zip;
            vendor2.Phone = vendor.Phone;
            vendor2.Email = vendor.Email;
            vendor2.IsPreapproved = vendor.IsPreapproved;


            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Vendor was changed."));
        }

        //Remove
        public ActionResult Remove([FromBody] Vendor vendor)
        {
            Vendor vendor2 = db.Vendors.Find(vendor.Id);
            db.Vendors.Remove(vendor2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Vendor was deleted"));
        }
    }
}