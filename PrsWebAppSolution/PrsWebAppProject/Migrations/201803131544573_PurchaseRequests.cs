namespace PrsWebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        Justification = c.String(nullable: false, maxLength: 255),
                        DeliveryMode = c.String(nullable: false, maxLength: 25),
                        Status = c.String(nullable: false, maxLength: 15),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        RejectionReason = c.String(maxLength: 80),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequests", "UserId", "dbo.Users");
            DropIndex("dbo.PurchaseRequests", new[] { "UserId" });
            DropTable("dbo.PurchaseRequests");
        }
    }
}
