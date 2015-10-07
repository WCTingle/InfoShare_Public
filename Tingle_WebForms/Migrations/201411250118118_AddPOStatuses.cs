namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPOStatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrderStatus",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            AddColumn("dbo.OrderCancellations", "POStatus_RecordId", c => c.Int());
            AddForeignKey("dbo.OrderCancellations", "POStatus_RecordId", "dbo.PurchaseOrderStatus", "RecordId");
            CreateIndex("dbo.OrderCancellations", "POStatus_RecordId");
            DropColumn("dbo.OrderCancellations", "StatusOfPO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderCancellations", "StatusOfPO", c => c.String(maxLength: 100));
            DropIndex("dbo.OrderCancellations", new[] { "POStatus_RecordId" });
            DropForeignKey("dbo.OrderCancellations", "POStatus_RecordId", "dbo.PurchaseOrderStatus");
            DropColumn("dbo.OrderCancellations", "POStatus_RecordId");
            DropTable("dbo.PurchaseOrderStatus");
        }
    }
}
