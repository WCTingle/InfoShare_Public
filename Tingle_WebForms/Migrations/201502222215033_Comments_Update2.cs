namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments_Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExpeditedOrderForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.ExpeditedOrderForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.Comments", "SystemComment", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.ExpeditedOrderForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.ExpeditedOrderForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            CreateIndex("dbo.ExpeditedOrderForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.ExpeditedOrderForms", "AssignedUser_SystemUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExpeditedOrderForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.ExpeditedOrderForms", new[] { "SubmittedUser_SystemUserID" });
            DropForeignKey("dbo.ExpeditedOrderForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.ExpeditedOrderForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropColumn("dbo.Comments", "SystemComment");
            DropColumn("dbo.ExpeditedOrderForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.ExpeditedOrderForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.ExpeditedOrderForms", "DueDate");
        }
    }
}
