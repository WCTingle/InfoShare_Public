namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Note = c.String(maxLength: 180),
                        FormId = c.Int(nullable: false),
                        User_SystemUserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.TForms", t => t.FormId, cascadeDelete: true)
                .ForeignKey("dbo.SystemUsers", t => t.User_SystemUserID)
                .Index(t => t.FormId)
                .Index(t => t.User_SystemUserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "User_SystemUserID" });
            DropIndex("dbo.Comments", new[] { "FormId" });
            DropForeignKey("dbo.Comments", "User_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.Comments", "FormId", "dbo.TForms");
            DropTable("dbo.Comments");
        }
    }
}
