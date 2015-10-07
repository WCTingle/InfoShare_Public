namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryApproval_AddMaxLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryApprovalForms", "Company", c => c.String(maxLength: 25));
            AlterColumn("dbo.InventoryApprovalForms", "PurchaseOrderNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.InventoryApprovalForms", "MaterialGroup", c => c.String(maxLength: 200));
            AlterColumn("dbo.InventoryApprovalForms", "ContainerNumber", c => c.String(maxLength: 25));
            AlterColumn("dbo.InventoryApprovalForms", "TimeToArrival", c => c.String(maxLength: 25));
            AlterColumn("dbo.InventoryApprovalStatus", "StatusDescription", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryApprovalStatus", "StatusDescription", c => c.String());
            AlterColumn("dbo.InventoryApprovalForms", "TimeToArrival", c => c.String());
            AlterColumn("dbo.InventoryApprovalForms", "ContainerNumber", c => c.String());
            AlterColumn("dbo.InventoryApprovalForms", "MaterialGroup", c => c.String());
            AlterColumn("dbo.InventoryApprovalForms", "PurchaseOrderNumber", c => c.String());
            AlterColumn("dbo.InventoryApprovalForms", "Company", c => c.String());
        }
    }
}
