using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsWebApp.Models
{

    public class User
    {

        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [StringLength(30)]
        [Required]
        public string Password { get; set; }

        [StringLength(30)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Required]
        public string LastName { get; set; }

        [StringLength(12)]
        [Required]
        public string Phone { get; set; }

        [StringLength(30)]
        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsReviewer { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateCreated { get; internal set; }
    }
}