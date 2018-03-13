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

namespace PrsWebAppProject.Controllers
{
    public class PurchaseRequestLineItemsController : Controller
    {
        private PrsDbContext db = new PrsDbContext();

        private void UpdatePurchaseRequestTotal(int prodid)
        {
            db = new PrsDbContext();

            decimal total = 0;
           var PurchaseRequestLineItems = db.PurchaseRequestLineItems.Where(p => p.PurchaseRequestId == prodid);
           foreach (var PurchaseRequestLineItem in PurchaseRequestLineItems)
           {
               var Total = PurchaseRequestLineItem.Quantity * PurchaseRequestLineItem.Product.Price;
           }
           var PurchaseRequest = db.PurchaseRequests.Find(prodid);
           PurchaseRequest.Total = total;
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
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            PurchaseRequestLineItem purchaserequestlineitem = db.PurchaseRequestLineItems.Find(id);
            if (purchaserequestlineitem == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = db.PurchaseRequestLineItems.Find(id) };
            //return Json(purchaserequest, JsonRequestBehavior.AllowGet);
        }
        // Create
        public ActionResult Create([System.Web.Http.FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            purchaserequestlineitem.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.PurchaseRequestLineItems.Add(purchaserequestlineitem);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return Json(new JsonMessage("Success", "Purchase Request Line Item was created,"));
        }

        //Change
        public ActionResult Change([FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
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
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return Json(new JsonMessage("Success", "Purchase Request Line Item was changed."));
        }

        //Remove
        public ActionResult Remove([FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            PurchaseRequestLineItem purchaserequestlineitem2 = db.PurchaseRequestLineItems.Find(purchaserequestlineitem.Id);
            db.PurchaseRequestLineItems.Remove(purchaserequestlineitem2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return Json(new JsonMessage("Success", "Purchase Request Line Items were deleted"));
        }
    }

}