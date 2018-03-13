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

namespace PrsWebAppProject.Controllers
{
    public class ProductsController : Controller
    {
            private PrsDbContext db = new PrsDbContext();

            //List
            public ActionResult List()
            {
                //return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);
                return new JsonNetResult { Data = db.Products.ToList() };

            }

            //Get
            public ActionResult Get(int? id)
            {
                if (id == null)
                {
                    return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
                }
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            // Create
            public ActionResult Create([System.Web.Http.FromBody] Product product)
            {
                product.DateCreated = DateTime.Now;
                if (!ModelState.IsValid)
                {
                    return Json(new JsonMessage("Failure", "Model State is not valid"), JsonRequestBehavior.AllowGet);
                }
                db.Products.Add(product);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonMessage("Success", "Product was created,"));
            }

            //Change
            public ActionResult Change([FromBody] Product product)
            {
                Product product2 = db.Products.Find(product.Id);
                product2.Id = product.Id;
                product2.VendorId = product.VendorId;
                product2.VendorPartNumber = product.VendorPartNumber;
                product2.Name = product.Name;
                product2.Price = product.Price;
                product2.Unit = product.Unit;
                product2.PhotoPath = product.PhotoPath;
                product2.Active = product.Active;
                product2.DateCreated = product.DateCreated;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonMessage("Success", "Product was changed."));
            }

            //Remove
            public ActionResult Remove([FromBody] Product product)
            {
                Product product2 = db.Products.Find(product.Id);
                db.Products.Remove(product2);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonMessage("Success", "Product was deleted"));
            }
    }
}
