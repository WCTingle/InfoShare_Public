namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSampleRequestForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SampleRequestForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        ProjectName = c.String(maxLength: 100),
                        ItemNumber = c.String(maxLength: 100),
                        Customer = c.String(maxLength: 100),
                        StyleNameColor = c.String(maxLength: 100),
                        AccountNumber = c.String(maxLength: 100),
                        Size = c.String(maxLength: 100),
                        Contact = c.String(maxLength: 100),
                        Quantity = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 100),
                        DealerAwareOfCost = c.Boolean(nullable: false),
                        DealerAwareOfFreight = c.Boolean(nullable: false),
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
            DropIndex("dbo.SampleRequestForms", new[] { "Status_StatusId" });
            DropForeignKey("dbo.SampleRequestForms", "Status_StatusId", "dbo.Status");
            DropTable("dbo.SampleRequestForms");
        }
    }
}
