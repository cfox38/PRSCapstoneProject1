using PrsCapstoneProject.Utility;
using PrsWebApp.Models;
using PrsWebAppProject.Models;
using PrsWebAppProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Utility;

namespace PrsWebAppProject.Controllers
{
    public class VendorsController : Controller
    {

        private PrsDbContext db = new PrsDbContext();

        //List
        public ActionResult List()
        {
            //return Json(db.Vendors.ToList(), JsonRequestBehavior.AllowGet);
            return new JsonNetResult{ Data = db.Vendors.ToList() };

        }

        //Get
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is null" ) };
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is null") };
            }
            return new JsonNetResult { Data = vendor };
        }
        // Create
        public ActionResult Create([System.Web.Http.FromBody] Vendor vendor)
        {
            ModelState.Remove("DateCreated");
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            vendor.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Model State is not valid") };
            }
            db.Vendors.Add(vendor);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult {Data = new JsonMessage("Failure", ex.Message)};
            }

            return new JsonNetResult { Data = new JsonMessage("Success", "Vendor was created.") };
        }

        //Change
        public ActionResult Change([FromBody] Vendor vendor)
        {
            ModelState.Remove("DateCreated");
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }

            if (vendor.Code == null) return new EmptyResult();
            Vendor vendor2 = db.Vendors.Find(vendor.Id);
            vendor2.Id = vendor.Id;
            vendor2.Code = vendor.Code;
            vendor2.Name = vendor.Name;
            vendor2.Address = vendor.Address;
            vendor2.City = vendor.City;
            vendor2.State = vendor.State;
            vendor2.Zip = vendor.Zip;
            vendor2.Phone = vendor.Phone;
            vendor2.Email = vendor.Email;
            vendor2.IsPreapproved = vendor.IsPreapproved;
            vendor2.Active = vendor.Active;
            vendor2.DateCreated = vendor.DateCreated;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) }; 
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "Vendor was changed.") };
        }


        //Remove
        public ActionResult Remove([FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }

            if (vendor.Code == null) return new EmptyResult();
            Vendor vendor2 = db.Vendors.Find(vendor.Id);
            db.Vendors.Remove(vendor2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
                return new JsonNetResult { Data = new JsonMessage("Success", "Vendor was deleted") };
                //return Json(new JsonMessage("Success", "Vendor was deleted"));
        }
    }
}