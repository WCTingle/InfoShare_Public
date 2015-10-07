namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderCancellation : DbMigration
    {
        public override void Up()
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
                        StatusOfPO = c.String(maxLength: 100),
                        SKU = c.String(maxLength: 100),
                        Line = c.String(maxLength: 100),
                        LineOfPO = c.String(maxLength: 100),
                        Size = c.String(maxLength: 100),
                        DateRequired = c.DateTime(nullable: false),
                        ShipVia = c.String(maxLength: 100),
                        Serial = c.String(maxLength: 100),
                        TruckRoute = c.String(maxLength: 100),
                        AdditionalInfo = c.String(maxLength: 2000),
                        CCFormToEmail = c.String(maxLength: 1000),
                        RequestHandler = c.String(maxLength: 100),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.Status_StatusId);
            
            AlterColumn("dbo.PriceChangeRequestForms", "Customer", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "LineNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "AccountNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "Quantity", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "SalesRep", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "Product", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "OrderNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "Price", c => c.String(maxLength: 100));
            AlterColumn("dbo.PriceChangeRequestForms", "CrossReferenceOldOrderNumber", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderCancellations", new[] { "Status_StatusId" });
            DropForeignKey("dbo.OrderCancellations", "Status_StatusId", "dbo.Status");
            AlterColumn("dbo.PriceChangeRequestForms", "CrossReferenceOldOrderNumber", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "Price", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "OrderNumber", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "Product", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "SalesRep", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "Quantity", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "AccountNumber", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "LineNumber", c => c.String());
            AlterColumn("dbo.PriceChangeRequestForms", "Customer", c => c.String());
            DropTable("dbo.OrderCancellations");
        }
    }
}
