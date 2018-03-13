using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsWebAppProject.Models
{

    public class Vendor
    {

        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [StringLength(30)]
        [Required]
        public string Address { get; set; }

        [StringLength(30)]
        [Required]
        public string City { get; set; }

        [StringLength(2)]
        [Required]
        public string State { get; set; }

        [StringLength(5)]
        [Required]
        public string Zip { get; set; }

        [StringLength(12)]
        [Required]
        public string Phone { get; set; }

        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsPreapproved { get; set; }

        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }

        }
    }