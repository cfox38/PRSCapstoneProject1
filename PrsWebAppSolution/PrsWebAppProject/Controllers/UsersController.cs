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
    public class UsersController : Controller
    {
        private PrsDbContext db = new PrsDbContext();

        //Get Username and password 
        public ActionResult Login(string UserName, string Password)
        {
            if (UserName == null || Password == null)
            {
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Invalid username/password." } };
            }
            var user = db.Users.SingleOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user == null)
            {
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "Invalid username/password." } };
            }
            return new JsonNetResult { Data = new Msg { Result = "Success", Message = "Login successful.", Data = user } };
        }

        //List
        public ActionResult List()
        {
            //return Json(db.Users.ToList(), JsonRequestBehavior.AllowGet);
            return new JsonNetResult { Data = db.Users.ToList() };
        }

        //Get
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is null") };
                //return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", "Id is not found") };
                //return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = user };
        }
        // Create [POST]
        public ActionResult Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            //if (user.UserName == null) return new EmptyResult();
            user.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return new JsonNetResult { Data = new JsonMessage("Fail", "ModelState invalid", ModelState) };
            }
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
            } catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "User was created") };
            //return Json(new JsonMessage("Success", "User was created,"));
        }

        //Change
        public ActionResult Change([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            if (user.UserName == null) return new EmptyResult();
            User user2 = db.Users.Find(user.Id);
            user2.Id = user.Id;
            user2.UserName = user.UserName;
            user2.Password = user.Password;
            user2.FirstName = user.FirstName;
            user2.LastName = user.LastName;
            user2.Phone = user.Phone;
            user2.Email = user.Email;
            user2.IsReviewer = user.IsReviewer;
            user2.IsAdmin = user.IsAdmin;
            user2.Active = user.Active;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "User was changed") };
            //return Json(new JsonMessage("Success", "User was changed."));
        }

        //Remove
        public ActionResult Remove([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrors.GetModelStateErrors(ModelState);
                return new JsonNetResult { Data = new Msg { Result = "Failed", Message = "ModelState invalid.", Data = errorMessages } };
            }
            if (user.UserName == null) return new EmptyResult();
            User user2 = db.Users.Find(user.Id);
            db.Users.Remove(user2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonNetResult { Data = new JsonMessage("Failure", ex.Message) };
                //return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return new JsonNetResult { Data = new JsonMessage("Success", "User was deleted") };
            //return Json(new JsonMessage("Success", "User was deleted"));
        }
    }
}