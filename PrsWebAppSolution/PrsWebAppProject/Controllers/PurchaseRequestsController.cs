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
using PrsCapstoneProject.Utility;

namespace PrsWebAppProject.Controllers
{
    public class PurchaseRequestsController : Controller
    {
        private PrsDbContext db = new PrsDbContext();

        //List
        public ActionResult List()
        {
            //return Json(db.PurchaseRequests.ToList(), JsonRequestBehavior.AllowGet);
            return new JsonNetResult { Data = db.PurchaseRequests.ToList() };
        }

        //ReviewList
        public ActionResult ReviewList()
        {
            return new JsonNetResult { Data = db.PurchaseRequests.Where(PurchaseRequest => PurchaseRequest.Status == "REVIEW").ToList() };
        } 
        //Get
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is null") };
                //return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            PurchaseRequest purchaserequest = db.PurchaseRequests.Find(id);
            if (purchaserequest == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is not found") };
                //return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = db.PurchaseRequests.Find(id) };
            //return Json(purchaserequest, JsonRequestBehavior.AllowGet);
        }
        // Create
        public ActionResult Create([System.Web.Http.FromBody] PurchaseRequest purchaserequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            purchaserequest.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Model State is not valid") };
                //return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.PurchaseRequests.Add(purchaserequest);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "Purchase request was created") };
            //return Json(new JsonMessage("Success", "Purchase Request was created,"));
        }

        //Change
        public ActionResult Change([FromBody] PurchaseRequest purchaserequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            PurchaseRequest purchaserequest2 = db.PurchaseRequests.Find(purchaserequest.Id);
            purchaserequest2.Id = purchaserequest.Id;
            purchaserequest2.UserId = purchaserequest.UserId;
            purchaserequest2.Description = purchaserequest.Description;
            purchaserequest2.Justification= purchaserequest.Justification;
            purchaserequest2.Status = purchaserequest.Status;
            purchaserequest2.Total = purchaserequest.Total;
            purchaserequest2.Active = purchaserequest.Active;
            purchaserequest2.RejectionReason = purchaserequest.RejectionReason;
            purchaserequest2.DateCreated = purchaserequest.DateCreated;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "ex.Message") };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "Purchase request was changed") };
            //return Json(new JsonMessage("Success", "Purchase Request was changed."));
        }

        //Remove
        public ActionResult Remove([FromBody] PurchaseRequest purchaserequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            //if (purchaserequest.Code == null) return new EmptyResult();
            PurchaseRequest purchaserequest2 = db.PurchaseRequests.Find(purchaserequest.Id);
            db.PurchaseRequests.Remove(purchaserequest2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "Purchase Request was deleted") };
            //return Json(new JsonMessage("Success", "Purchase Request was deleted"));
        }
    }
}
