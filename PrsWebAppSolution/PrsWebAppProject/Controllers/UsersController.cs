using PrsWebAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrsWebAppProject.Controllers
{
    public class UsersController : Controller
    {
        private PrsDbContext db = new PrsDbContext();
        
        public ActionResult List()
        {
            return Json(db.Users.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}