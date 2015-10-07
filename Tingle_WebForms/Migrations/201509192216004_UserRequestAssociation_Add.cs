namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRequestAssociation_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequestAssociations",
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
            DropForeignKey("dbo.UserRequestAssociations", "User_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.UserRequestAssociations", "Form_FormID", "dbo.TForms");
            DropIndex("dbo.UserRequestAssociations", new[] { "User_SystemUserID" });
            DropIndex("dbo.UserRequestAssociations", new[] { "Form_FormID" });
            DropTable("dbo.UserRequestAssociations");
        }
    }
}
