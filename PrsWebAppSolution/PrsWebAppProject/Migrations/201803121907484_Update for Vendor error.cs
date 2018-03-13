namespace PrsWebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateforVendorerror : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "VendorPartNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Products", "Unit", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Products", "PhotoPath", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PhotoPath", c => c.String(maxLength: 130));
            AlterColumn("dbo.Products", "Unit", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Products", "VendorPartNumber", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
