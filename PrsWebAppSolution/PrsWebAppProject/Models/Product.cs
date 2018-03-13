using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsWebAppProject.Models
{
    public class Product
    {

        public int Id { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        [StringLength(50)]
        [Required]
        public string VendorPartNumber { get; set; }

        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(255)]
        [Required]
        public string Unit { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }
        
        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

    }
}