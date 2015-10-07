namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderCancellations", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.OrderCancellations", "POStatus_RecordId", "dbo.PurchaseOrderStatus");
            DropIndex("dbo.OrderCancellations", new[] { "Status_StatusId" });
            DropIndex("dbo.OrderCancellations", new[] { "POStatus_RecordId" });
            CreateTable(
                "dbo.OrderCancellationForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        OrderNumber = c.String(maxLength: 100),
                        ArmstrongReference = c.String(maxLength: 100),
                        Customer = c.String(maxLength: 100),
                        PO = c.String(maxLength: 100),
                        SKU = c.String(maxLength: 100),
                        Line = c.String(maxLength: 100),
                        LineOfPO = c.String(maxLength: 100),
                        Size = c.String(maxLength: 100),
                        DateRequired = c.DateTime(nullable: false),
                        ShipVia = c.String(maxLength: 100),
                        Serial = c.String(maxLength: 100),
                        TruckRoute = c.String(maxLength: 100),
                        AdditionalInfo = c.String(maxLength: 2000),
                        Company = c.String(maxLength: 10),
                        CCFormToEmail = c.String(maxLength: 1000),
                        RequestHandler = c.String(maxLength: 100),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        Status_StatusId = c.Int(),
                        POStatus_RecordId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .ForeignKey("dbo.PurchaseOrderStatus", t => t.POStatus_RecordId)
                .Index(t => t.Status_StatusId)
                .Index(t => t.POStatus_RecordId);

            RenameColumn("dbo.ExpeditedOrderForms", "ExpeditedOrderFormID", "RecordId");
            DropTable("dbo.OrderCancellations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderCancellations",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        OrderNumber = c.String(maxLength: 100),
                        ArmstrongReference = c.String(maxLength: 100),
                        Customer = c.String(maxLength: 100),
                        PO = c.String(maxLength: 100),
                        SKU = c.String(maxLength: 100),
                        Line = c.String(maxLength: 100),
                        LineOfPO = c.String(maxLength: 100),
                        Size = c.String(maxLength: 100),
                        DateRequired = c.DateTime(nullable: false),
                        ShipVia = c.String(maxLength: 100),
                        Serial = c.String(maxLength: 100),
                        TruckRoute = c.String(maxLength: 100),
                        AdditionalInfo = c.String(maxLength: 2000),
                        Company = c.String(maxLength: 10),
                        CCFormToEmail = c.String(maxLength: 1000),
                        RequestHandler = c.String(maxLength: 100),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        Status_StatusId = c.Int(),
                        POStatus_RecordId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId);

            RenameColumn("dbo.ExpeditedOrderForms", "RecordId", "ExpeditedOrderFormID");
            DropIndex("dbo.OrderCancellationForms", new[] { "POStatus_RecordId" });
            DropIndex("dbo.OrderCancellationForms", new[] { "Status_StatusId" });
            DropForeignKey("dbo.OrderCancellationForms", "POStatus_RecordId", "dbo.PurchaseOrderStatus");
            DropForeignKey("dbo.OrderCancellationForms", "Status_StatusId", "dbo.Status");
            DropTable("dbo.OrderCancellationForms");
            CreateIndex("dbo.OrderCancellations", "POStatus_RecordId");
            CreateIndex("dbo.OrderCancellations", "Status_StatusId");
            AddForeignKey("dbo.OrderCancellations", "POStatus_RecordId", "dbo.PurchaseOrderStatus", "RecordId");
            AddForeignKey("dbo.OrderCancellations", "Status_StatusId", "dbo.Status", "StatusId");
        }
    }
}
