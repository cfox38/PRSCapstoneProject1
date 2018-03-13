namespace PrsWebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedActiveandDateCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        VendorPartNumber = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false, maxLength: 30),
                        Price = c.Double(nullable: false),
                        Unit = c.String(nullable: false, maxLength: 30),
                        PhotoPath = c.String(maxLength: 130),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
            AddColumn("dbo.Users", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vendors", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vendors", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "VendorId", "dbo.Vendors");
            DropIndex("dbo.Products", new[] { "VendorId" });
            DropColumn("dbo.Vendors", "DateCreated");
            DropColumn("dbo.Vendors", "Active");
            DropColumn("dbo.Users", "DateCreated");
            DropTable("dbo.Products");
        }
    }
}
