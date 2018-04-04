using Newtonsoft.Json;
using PrsWebApp.Models;
using PrsWebAppProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsWebApp.Models
{

    public class PurchaseRequestLineItem
    {

        public int Id { get; set; }

        public int PurchaseRequestId { get; set; }
        [JsonIgnore]
        public virtual PurchaseRequest PurchaseRequest { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }
        public object Products { get; internal set; }
        //public object Code { get; internal set; }
    }
} 