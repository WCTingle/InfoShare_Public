namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMustIncludeForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MustIncludeForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        PO = c.String(maxLength: 100),
                        ArmstrongReference = c.String(maxLength: 100),
                        Pattern = c.String(maxLength: 100),
                        Line = c.String(maxLength: 100),
                        OrderNumber = c.String(maxLength: 100),
                        Customer = c.String(maxLength: 100),
                        Warehouse = c.String(maxLength: 100),
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
            DropIndex("dbo.MustIncludeForms", new[] { "Status_StatusId" });
            DropForeignKey("dbo.MustIncludeForms", "Status_StatusId", "dbo.Status");
            DropTable("dbo.MustIncludeForms");
        }
    }
}
