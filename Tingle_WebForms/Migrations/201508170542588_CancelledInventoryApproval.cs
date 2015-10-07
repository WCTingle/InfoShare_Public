namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CancelledInventoryApproval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryApprovalForms", "CancelledDate", c => c.DateTime());
            AddColumn("dbo.InventoryApprovalForms", "CancelledBy_SystemUserID", c => c.Int());
            CreateIndex("dbo.InventoryApprovalForms", "CancelledBy_SystemUserID");
            AddForeignKey("dbo.InventoryApprovalForms", "CancelledBy_SystemUserID", "dbo.SystemUsers", "SystemUserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryApprovalForms", "CancelledBy_SystemUserID", "dbo.SystemUsers");
            DropIndex("dbo.InventoryApprovalForms", new[] { "CancelledBy_SystemUserID" });
            DropColumn("dbo.InventoryApprovalForms", "CancelledBy_SystemUserID");
            DropColumn("dbo.InventoryApprovalForms", "CancelledDate");
        }
    }
}
