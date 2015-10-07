namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultiplePOStatuses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderCancellationForms", "POStatus_RecordId", "dbo.PurchaseOrderStatus");
            DropIndex("dbo.OrderCancellationForms", new[] { "POStatus_RecordId" });
            AddColumn("dbo.OrderCancellationForms", "POStatusList", c => c.String());
            DropColumn("dbo.OrderCancellationForms", "POStatus_RecordId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderCancellationForms", "POStatus_RecordId", c => c.Int());
            DropColumn("dbo.OrderCancellationForms", "POStatusList");
            CreateIndex("dbo.OrderCancellationForms", "POStatus_RecordId");
            AddForeignKey("dbo.OrderCancellationForms", "POStatus_RecordId", "dbo.PurchaseOrderStatus", "RecordId");
        }
    }
}
