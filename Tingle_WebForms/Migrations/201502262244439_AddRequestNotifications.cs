namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestNotifications",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        RequestedFormId = c.Int(nullable: false),
                        EmailList = c.String(),
                        Form_FormID = c.Int(),
                        SentBy_SystemUserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.TForms", t => t.Form_FormID)
                .ForeignKey("dbo.SystemUsers", t => t.SentBy_SystemUserID)
                .Index(t => t.Form_FormID)
                .Index(t => t.SentBy_SystemUserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestNotifications", new[] { "SentBy_SystemUserID" });
            DropIndex("dbo.RequestNotifications", new[] { "Form_FormID" });
            DropForeignKey("dbo.RequestNotifications", "SentBy_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.RequestNotifications", "Form_FormID", "dbo.TForms");
            DropTable("dbo.RequestNotifications");
        }
    }
}
