using PrsWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsWebAppProject.Models
{
    public class PurchaseRequest
    {

        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        [StringLength(255)]
        [Required]
        public string Justification { get; set; }

        [StringLength(25)]
        [Required]
        public string DeliveryMode { get; set; }

        [StringLength(15)]
        [Required]
        public string Status { get; set; }

        [Required]
        public decimal Total { get; set; }

        public bool Active { get; set; }

        [StringLength(80)]
        public string RejectionReason { get; set; }

        public DateTime DateCreated { get; set; }

    }
}