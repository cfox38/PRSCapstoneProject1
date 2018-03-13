using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PrsWebApp.Models;

namespace PrsWebAppProject.Models
{
    public class PrsDbContext : DbContext
    {
        public PrsDbContext() : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
    }
}