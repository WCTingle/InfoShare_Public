namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestForCheckForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestForCheckForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        PayableTo = c.String(maxLength: 100),
                        ChargeToAccountNumber = c.String(maxLength: 100),
                        ChargeToOther = c.String(maxLength: 100),
                        Amount = c.String(maxLength: 100),
                        For = c.String(maxLength: 100),
                        RequestedBy = c.String(maxLength: 100),
                        ApprovedBy = c.String(maxLength: 100),
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
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.Status_StatusId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestForCheckForms", new[] { "Status_StatusId" });
            DropForeignKey("dbo.RequestForCheckForms", "Status_StatusId", "dbo.Status");
            DropTable("dbo.RequestForCheckForms");
        }
    }
}
