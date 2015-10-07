namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHotRushForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotRushForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Customer = c.String(maxLength: 100),
                        EntireOrderOrLineNumber = c.String(maxLength: 500),
                        OrderNumber = c.String(maxLength: 100),
                        CreditRelease = c.Boolean(nullable: false),
                        OrderAcknowledgement = c.Boolean(nullable: false),
                        InstallDate = c.DateTime(nullable: false),
                        Company = c.String(maxLength: 10),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        CCFormToEmail = c.String(maxLength: 1000),
                        AdditionalInfo = c.String(maxLength: 2000),
                        RequestHandler = c.String(maxLength: 100),
                        CompletedNotes = c.String(maxLength: 4000),
                        CCCompletedFormToEmail = c.String(maxLength: 1000),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.Status_StatusId);
            
            AddColumn("dbo.PriceChangeRequestForms", "CompletedNotes", c => c.String(maxLength: 4000));
            AddColumn("dbo.PriceChangeRequestForms", "CCCompletedFormToEmail", c => c.String(maxLength: 1000));
            AddColumn("dbo.OrderCancellationForms", "CompletedNotes", c => c.String(maxLength: 4000));
            AddColumn("dbo.OrderCancellationForms", "CCCompletedFormToEmail", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropIndex("dbo.HotRushForms", new[] { "Status_StatusId" });
            DropForeignKey("dbo.HotRushForms", "Status_StatusId", "dbo.Status");
            DropColumn("dbo.OrderCancellationForms", "CCCompletedFormToEmail");
            DropColumn("dbo.OrderCancellationForms", "CompletedNotes");
            DropColumn("dbo.PriceChangeRequestForms", "CCCompletedFormToEmail");
            DropColumn("dbo.PriceChangeRequestForms", "CompletedNotes");
            DropTable("dbo.HotRushForms");
        }
    }
}
