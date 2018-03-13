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

        public ActionResult Login(string UserName, string Password)
        {
            var users = db.Users.Where(u => u.UserName == UserName && u.Password == Password);
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
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
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(user, JsonRequestBehavior.AllowGet); 
        }
        // Create
        public ActionResult Create([System.Web.Http.FromBody] User user)
        {
            user.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
            } catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "User was created,"));
        }

        //Change
        public ActionResult Change([FromBody] User user)
        {
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
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "User was changed."));
        }

        //Remove
        public ActionResult Remove([FromBody] User user)
        {
            User user2 = db.Users.Find(user.Id);
            db.Users.Remove(user2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "User was deleted"));
        }
    }
}