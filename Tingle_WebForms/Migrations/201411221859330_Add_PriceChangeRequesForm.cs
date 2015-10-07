namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PriceChangeRequesForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceChangeRequestForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Customer = c.String(),
                        LineNumber = c.String(),
                        AccountNumber = c.String(),
                        Quantity = c.String(),
                        SalesRep = c.String(),
                        Product = c.String(),
                        OrderNumber = c.String(),
                        Price = c.String(),
                        CrossReferenceOldOrderNumber = c.String(),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        CCFormToEmail = c.String(maxLength: 1000),
                        AdditionalInfo = c.String(maxLength: 2000),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.Status_StatusId);
            
            AlterColumn("dbo.ExpeditedOrderForms", "CCFormToEmail", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropIndex("dbo.PriceChangeRequestForms", new[] { "Status_StatusId" });
            DropForeignKey("dbo.PriceChangeRequestForms", "Status_StatusId", "dbo.Status");
            AlterColumn("dbo.ExpeditedOrderForms", "CCFormToEmail", c => c.String(maxLength: 100));
            DropTable("dbo.PriceChangeRequestForms");
        }
    }
}
