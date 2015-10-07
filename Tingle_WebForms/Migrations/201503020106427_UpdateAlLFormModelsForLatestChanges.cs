namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAlLFormModelsForLatestChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.ExpeditedOrderForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.ExpeditedOrderForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.PriceChangeRequestForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.PriceChangeRequestForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.PriceChangeRequestForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.OrderCancellationForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.OrderCancellationForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.OrderCancellationForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.HotRushForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.HotRushForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.HotRushForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.LowInventoryForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.LowInventoryForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.LowInventoryForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.SampleRequestForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.SampleRequestForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.SampleRequestForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.RequestForCheckForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.RequestForCheckForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.RequestForCheckForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.MustIncludeForms", "Priority", c => c.String(maxLength: 10));
            AddColumn("dbo.MustIncludeForms", "RequestedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.MustIncludeForms", "LastModifiedUser_SystemUserID", c => c.Int());
            AlterColumn("dbo.ExpeditedOrderForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.PriceChangeRequestForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.OrderCancellationForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.HotRushForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.LowInventoryForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.SampleRequestForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.RequestForCheckForms", "DueDate", c => c.DateTime());
            AlterColumn("dbo.MustIncludeForms", "DueDate", c => c.DateTime());
            AddForeignKey("dbo.ExpeditedOrderForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.ExpeditedOrderForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.PriceChangeRequestForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.PriceChangeRequestForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.OrderCancellationForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.OrderCancellationForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.HotRushForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.HotRushForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.LowInventoryForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.LowInventoryForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.SampleRequestForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.SampleRequestForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.RequestForCheckForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.RequestForCheckForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.MustIncludeForms", "RequestedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.MustIncludeForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            CreateIndex("dbo.ExpeditedOrderForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.ExpeditedOrderForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.PriceChangeRequestForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.PriceChangeRequestForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.OrderCancellationForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.OrderCancellationForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.HotRushForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.HotRushForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.LowInventoryForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.LowInventoryForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.SampleRequestForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.SampleRequestForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.RequestForCheckForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.RequestForCheckForms", "LastModifiedUser_SystemUserID");
            CreateIndex("dbo.MustIncludeForms", "RequestedUser_SystemUserID");
            CreateIndex("dbo.MustIncludeForms", "LastModifiedUser_SystemUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MustIncludeForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.MustIncludeForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.RequestForCheckForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.RequestForCheckForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.SampleRequestForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.SampleRequestForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.LowInventoryForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.LowInventoryForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.HotRushForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.HotRushForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.OrderCancellationForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.OrderCancellationForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.PriceChangeRequestForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.PriceChangeRequestForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.ExpeditedOrderForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.ExpeditedOrderForms", new[] { "RequestedUser_SystemUserID" });
            DropForeignKey("dbo.MustIncludeForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.MustIncludeForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.RequestForCheckForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.RequestForCheckForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.SampleRequestForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.SampleRequestForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.LowInventoryForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.LowInventoryForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.HotRushForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.HotRushForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.OrderCancellationForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.OrderCancellationForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.PriceChangeRequestForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.PriceChangeRequestForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.ExpeditedOrderForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.ExpeditedOrderForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            AlterColumn("dbo.MustIncludeForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RequestForCheckForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SampleRequestForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LowInventoryForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HotRushForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderCancellationForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PriceChangeRequestForms", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExpeditedOrderForms", "DueDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.MustIncludeForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.MustIncludeForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.MustIncludeForms", "Priority");
            DropColumn("dbo.RequestForCheckForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.RequestForCheckForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.RequestForCheckForms", "Priority");
            DropColumn("dbo.SampleRequestForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.SampleRequestForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.SampleRequestForms", "Priority");
            DropColumn("dbo.LowInventoryForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.LowInventoryForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.LowInventoryForms", "Priority");
            DropColumn("dbo.HotRushForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.HotRushForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.HotRushForms", "Priority");
            DropColumn("dbo.OrderCancellationForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.OrderCancellationForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.OrderCancellationForms", "Priority");
            DropColumn("dbo.PriceChangeRequestForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.PriceChangeRequestForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.PriceChangeRequestForms", "Priority");
            DropColumn("dbo.ExpeditedOrderForms", "LastModifiedUser_SystemUserID");
            DropColumn("dbo.ExpeditedOrderForms", "RequestedUser_SystemUserID");
            DropColumn("dbo.ExpeditedOrderForms", "Priority");
        }
    }
}
