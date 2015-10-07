namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriorityModelUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.PriceChangeRequestForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.OrderCancellationForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.HotRushForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.LowInventoryForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.SampleRequestForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.DirectOrderForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.RequestForCheckForms", "Priority_RecordId", c => c.Int());
            AddColumn("dbo.MustIncludeForms", "Priority_RecordId", c => c.Int());
            AddForeignKey("dbo.ExpeditedOrderForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.PriceChangeRequestForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.OrderCancellationForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.HotRushForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.LowInventoryForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.SampleRequestForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.DirectOrderForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.RequestForCheckForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            AddForeignKey("dbo.MustIncludeForms", "Priority_RecordId", "dbo.Priorities", "RecordId");
            CreateIndex("dbo.ExpeditedOrderForms", "Priority_RecordId");
            CreateIndex("dbo.PriceChangeRequestForms", "Priority_RecordId");
            CreateIndex("dbo.OrderCancellationForms", "Priority_RecordId");
            CreateIndex("dbo.HotRushForms", "Priority_RecordId");
            CreateIndex("dbo.LowInventoryForms", "Priority_RecordId");
            CreateIndex("dbo.SampleRequestForms", "Priority_RecordId");
            CreateIndex("dbo.DirectOrderForms", "Priority_RecordId");
            CreateIndex("dbo.RequestForCheckForms", "Priority_RecordId");
            CreateIndex("dbo.MustIncludeForms", "Priority_RecordId");
            DropColumn("dbo.ExpeditedOrderForms", "Priority");
            DropColumn("dbo.PriceChangeRequestForms", "Priority");
            DropColumn("dbo.OrderCancellationForms", "Priority");
            DropColumn("dbo.HotRushForms", "Priority");
            DropColumn("dbo.LowInventoryForms", "Priority");
            DropColumn("dbo.SampleRequestForms", "Priority");
            DropColumn("dbo.DirectOrderForms", "Priority");
            DropColumn("dbo.RequestForCheckForms", "Priority");
            DropColumn("dbo.MustIncludeForms", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MustIncludeForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.RequestForCheckForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.DirectOrderForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.SampleRequestForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.LowInventoryForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.HotRushForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.OrderCancellationForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.PriceChangeRequestForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.ExpeditedOrderForms", "Priority", c => c.String(maxLength: 10));
            DropIndex("dbo.MustIncludeForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.RequestForCheckForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.DirectOrderForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.SampleRequestForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.LowInventoryForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.HotRushForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.OrderCancellationForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.PriceChangeRequestForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.ExpeditedOrderForms", new[] { "Priority_RecordId" });
            DropForeignKey("dbo.MustIncludeForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.RequestForCheckForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.DirectOrderForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.SampleRequestForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.LowInventoryForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.HotRushForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.OrderCancellationForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.PriceChangeRequestForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.ExpeditedOrderForms", "Priority_RecordId", "dbo.Priorities");
            DropColumn("dbo.MustIncludeForms", "Priority_RecordId");
            DropColumn("dbo.RequestForCheckForms", "Priority_RecordId");
            DropColumn("dbo.DirectOrderForms", "Priority_RecordId");
            DropColumn("dbo.SampleRequestForms", "Priority_RecordId");
            DropColumn("dbo.LowInventoryForms", "Priority_RecordId");
            DropColumn("dbo.HotRushForms", "Priority_RecordId");
            DropColumn("dbo.OrderCancellationForms", "Priority_RecordId");
            DropColumn("dbo.PriceChangeRequestForms", "Priority_RecordId");
            DropColumn("dbo.ExpeditedOrderForms", "Priority_RecordId");
        }
    }
}
