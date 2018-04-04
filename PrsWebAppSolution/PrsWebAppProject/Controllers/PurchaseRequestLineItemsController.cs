using PrsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PrsWebAppProject.Utility;
using PrsWebAppProject.Models;
using Utility;
using System.Data.Entity;
using PrsCapstoneProject.Utility;

namespace PrsWebAppProject.Controllers
{
    public class PurchaseRequestLineItemsController : Controller
    {
        private PrsDbContext db = new PrsDbContext();

        private void UpdatePurchaseRequestTotal(int id)
        {
            db = new PrsDbContext();
            var PurchaseRequest = db.PurchaseRequests.Find(id);


            PurchaseRequest.Total = db.PurchaseRequestLineItems.Where(p => p.PurchaseRequestId == PurchaseRequest.Id).Sum(p1 => p1.Product.Price * p1.Quantity);      
           db.SaveChanges();
       }

       //List
            public ActionResult List()
        {
            //return Json(db.PurchaseRequestLineItems.ToList(), JsonRequestBehavior.AllowGet);
            return new JsonNetResult { Data = db.PurchaseRequestLineItems.ToList() };

        }

        //Get
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is null") };
                //return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            PurchaseRequestLineItem purchaserequestlineitem = db.PurchaseRequestLineItems.Find(id);
            if (purchaserequestlineitem == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is not found") };
                //return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = db.PurchaseRequestLineItems.Find(id) };
            //return Json(purchaserequest, JsonRequestBehavior.AllowGet);
        }
        // Create
        public ActionResult Create([System.Web.Http.FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            if (purchaserequestlineitem.ProductId == null) return new EmptyResult();
            purchaserequestlineitem.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
           
            
            db.PurchaseRequestLineItems.Add(purchaserequestlineitem);
            try
            {
               
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);



            return new JsonNetResult { Data = new JsonMessage("Success", "Purchase Request Line Item was created", purchaserequestlineitem.Id) };

            //return Json(new JsonMessage("Success", "Purchase Request Line Item was created,"));
        }

        //Change
        public ActionResult Change([FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            if (purchaserequestlineitem.Id == 0) return new EmptyResult();
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            PurchaseRequestLineItem purchaserequestlineitem2 = db.PurchaseRequestLineItems.Find(purchaserequestlineitem.Id);
            purchaserequestlineitem2.Id = purchaserequestlineitem.Id;
            purchaserequestlineitem2.PurchaseRequestId = purchaserequestlineitem.PurchaseRequestId;
            purchaserequestlineitem2.ProductId = purchaserequestlineitem.ProductId;
            purchaserequestlineitem2.Quantity = purchaserequestlineitem.Quantity;
            purchaserequestlineitem2.Active = purchaserequestlineitem.Active;
            purchaserequestlineitem2.DateCreated = purchaserequestlineitem.DateCreated;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return new JsonNetResult { Data = new JsonMessage("Success", "Purchase Request Line Item was changed") };
            //return Json(new JsonMessage("Success", "Purchase Request Line Item was changed."));
        }

        //Remove
        public ActionResult Remove([FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            if (purchaserequestlineitem.Id == 0) return new EmptyResult();
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            if (purchaserequestlineitem.Id == 0) return new EmptyResult();
            PurchaseRequestLineItem purchaserequestlineitem2 = db.PurchaseRequestLineItems.Find(purchaserequestlineitem.Id);
            db.PurchaseRequestLineItems.Remove(purchaserequestlineitem2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return new JsonNetResult { Data = new JsonMessage("Success", "Purchase Request Line Items were deleted") };
            //return Json(new JsonMessage("Success", "Purchase Request Line Items were deleted"));
        }
    }

}