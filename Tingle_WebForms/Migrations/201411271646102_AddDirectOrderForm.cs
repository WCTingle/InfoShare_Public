namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirectOrderForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DirectOrderForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        OowOrderNumber = c.String(maxLength: 100),
                        Customer = c.String(maxLength: 100),
                        AccountNumber = c.String(maxLength: 6),
                        PurchaseOrderNumber = c.String(maxLength: 100),
                        MaterialSku = c.String(maxLength: 100),
                        QuantityOrdered = c.String(),
                        InstallDate = c.DateTime(),
                        SM = c.String(maxLength: 100),
                        ContactName = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 100),
                        ShipVia = c.String(maxLength: 100),
                        Reserve = c.String(maxLength: 100),
                        ShipToName = c.String(maxLength: 100),
                        ShipToAddress = c.String(maxLength: 100),
                        ShipToCity = c.String(maxLength: 100),
                        ShipToState = c.String(maxLength: 100),
                        ShipToZip = c.String(maxLength: 100),
                        AdditionalInfo = c.String(maxLength: 2000),
                        RequestHandler = c.String(maxLength: 100),
                        Company = c.String(maxLength: 10),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        CCFormToEmail = c.String(maxLength: 1000),
                        CompletedNotes = c.String(maxLength: 4000),
                        CCCompletedFormToEmail = c.String(maxLength: 1000),
                        ExpediteCode_ExpediteCodeID = c.Int(nullable: false),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.ExpediteCodes", t => t.ExpediteCode_ExpediteCodeID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.ExpediteCode_ExpediteCodeID)
                .Index(t => t.Status_StatusId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DirectOrderForms", new[] { "Status_StatusId" });
            DropIndex("dbo.DirectOrderForms", new[] { "ExpediteCode_ExpediteCodeID" });
            DropForeignKey("dbo.DirectOrderForms", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.DirectOrderForms", "ExpediteCode_ExpediteCodeID", "dbo.ExpediteCodes");
            DropTable("dbo.DirectOrderForms");
        }
    }
}
