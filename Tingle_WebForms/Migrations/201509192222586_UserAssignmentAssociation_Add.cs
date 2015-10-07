namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAssignmentAssociation_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAssignmentAssociations",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        RelatedFormId = c.Int(nullable: false),
                        Form_FormID = c.Int(),
                        User_SystemUserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.TForms", t => t.Form_FormID)
                .ForeignKey("dbo.SystemUsers", t => t.User_SystemUserID)
                .Index(t => t.Form_FormID)
                .Index(t => t.User_SystemUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAssignmentAssociations", "User_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.UserAssignmentAssociations", "Form_FormID", "dbo.TForms");
            DropIndex("dbo.UserAssignmentAssociations", new[] { "User_SystemUserID" });
            DropIndex("dbo.UserAssignmentAssociations", new[] { "Form_FormID" });
            DropTable("dbo.UserAssignmentAssociations");
        }
    }
}
