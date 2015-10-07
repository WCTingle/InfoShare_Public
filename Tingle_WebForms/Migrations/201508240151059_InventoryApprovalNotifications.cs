namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryApprovalNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryApprovalNotifications",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        ToEmailAddress = c.String(),
                        BodyHtml = c.String(),
                        Status = c.Short(nullable: false),
                        SentBy_SystemUserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.SentBy_SystemUserID)
                .Index(t => t.SentBy_SystemUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryApprovalNotifications", "SentBy_SystemUserID", "dbo.SystemUsers");
            DropIndex("dbo.InventoryApprovalNotifications", new[] { "SentBy_SystemUserID" });
            DropTable("dbo.InventoryApprovalNotifications");
        }
    }
}
