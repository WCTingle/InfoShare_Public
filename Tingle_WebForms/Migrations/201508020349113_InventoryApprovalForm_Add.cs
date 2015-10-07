namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryApprovalForm_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryApprovalForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Company = c.String(),
                        PurchaseOrderNumber = c.String(),
                        MaterialGroup = c.String(),
                        Cost = c.String(),
                        ContainerNumber = c.String(),
                        EstimatedShipDate = c.DateTime(),
                        EstimatedTimeOfArrival = c.DateTime(),
                        ApprovedDate = c.DateTime(),
                        ActualShipDate = c.DateTime(),
                        OrderDate = c.DateTime(),
                        ArrivalDate = c.DateTime(),
                        TimeToArrival = c.String(),
                        InvoiceDate = c.DateTime(),
                        LastModifiedTimestamp = c.DateTime(nullable: false),
                        ApprovedBy_SystemUserID = c.Int(),
                        InvoicedBy_SystemUserID = c.Int(),
                        LastModifiedUser_SystemUserID = c.Int(),
                        OrderedBy_SystemUserID = c.Int(),
                        Priority_RecordId = c.Int(),
                        ReceivedBy_SystemUserID = c.Int(),
                        Status_RecordId = c.Int(),
                        SubmittedUser_SystemUserID = c.Int(),
                        Vendor_RecordId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.ApprovedBy_SystemUserID)
                .ForeignKey("dbo.SystemUsers", t => t.InvoicedBy_SystemUserID)
                .ForeignKey("dbo.SystemUsers", t => t.LastModifiedUser_SystemUserID)
                .ForeignKey("dbo.SystemUsers", t => t.OrderedBy_SystemUserID)
                .ForeignKey("dbo.Priorities", t => t.Priority_RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.ReceivedBy_SystemUserID)
                .ForeignKey("dbo.InventoryApprovalStatus", t => t.Status_RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.SubmittedUser_SystemUserID)
                .ForeignKey("dbo.Vendors", t => t.Vendor_RecordId)
                .Index(t => t.ApprovedBy_SystemUserID)
                .Index(t => t.InvoicedBy_SystemUserID)
                .Index(t => t.LastModifiedUser_SystemUserID)
                .Index(t => t.OrderedBy_SystemUserID)
                .Index(t => t.Priority_RecordId)
                .Index(t => t.ReceivedBy_SystemUserID)
                .Index(t => t.Status_RecordId)
                .Index(t => t.SubmittedUser_SystemUserID)
                .Index(t => t.Vendor_RecordId);
            
            CreateTable(
                "dbo.InventoryApprovalStatus",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        StatusDescription = c.String(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        VendorName = c.String(maxLength: 100),
                        VendorAddress = c.String(maxLength: 100),
                        VendorCity = c.String(maxLength: 50),
                        VendorState = c.String(maxLength: 20),
                        VendorZip = c.String(maxLength: 15),
                        VendorPhone = c.String(maxLength: 20),
                        VendorFax = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.RecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryApprovalForms", "Vendor_RecordId", "dbo.Vendors");
            DropForeignKey("dbo.InventoryApprovalForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.InventoryApprovalForms", "Status_RecordId", "dbo.InventoryApprovalStatus");
            DropForeignKey("dbo.InventoryApprovalForms", "ReceivedBy_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.InventoryApprovalForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.InventoryApprovalForms", "OrderedBy_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.InventoryApprovalForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.InventoryApprovalForms", "InvoicedBy_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.InventoryApprovalForms", "ApprovedBy_SystemUserID", "dbo.SystemUsers");
            DropIndex("dbo.InventoryApprovalForms", new[] { "Vendor_RecordId" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "Status_RecordId" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "ReceivedBy_SystemUserID" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "OrderedBy_SystemUserID" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "InvoicedBy_SystemUserID" });
            DropIndex("dbo.InventoryApprovalForms", new[] { "ApprovedBy_SystemUserID" });
            DropTable("dbo.Vendors");
            DropTable("dbo.InventoryApprovalStatus");
            DropTable("dbo.InventoryApprovalForms");
        }
    }
}
