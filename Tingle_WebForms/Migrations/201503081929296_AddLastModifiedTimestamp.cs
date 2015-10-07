namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastModifiedTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.PriceChangeRequestForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderCancellationForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.HotRushForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.LowInventoryForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.SampleRequestForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.DirectOrderForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestForCheckForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.MustIncludeForms", "LastModifiedTimestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MustIncludeForms", "LastModifiedTimestamp");
            DropColumn("dbo.RequestForCheckForms", "LastModifiedTimestamp");
            DropColumn("dbo.DirectOrderForms", "LastModifiedTimestamp");
            DropColumn("dbo.SampleRequestForms", "LastModifiedTimestamp");
            DropColumn("dbo.LowInventoryForms", "LastModifiedTimestamp");
            DropColumn("dbo.HotRushForms", "LastModifiedTimestamp");
            DropColumn("dbo.OrderCancellationForms", "LastModifiedTimestamp");
            DropColumn("dbo.PriceChangeRequestForms", "LastModifiedTimestamp");
            DropColumn("dbo.ExpeditedOrderForms", "LastModifiedTimestamp");
        }
    }
}
