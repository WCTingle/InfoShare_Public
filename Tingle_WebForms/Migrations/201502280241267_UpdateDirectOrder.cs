namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DirectOrderForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.DirectOrderForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddForeignKey("dbo.DirectOrderForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.DirectOrderForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            CreateIndex("dbo.DirectOrderForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.DirectOrderForms", "LastModifiedUser_SystemUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DirectOrderForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.DirectOrderForms", new[] { "RequestedUser_SystemUserID" });
            DropForeignKey("dbo.DirectOrderForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.DirectOrderForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropColumn("dbo.DirectOrderForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.DirectOrderForms", "RequestedUser_SystemUserID");
        }
    }
}
