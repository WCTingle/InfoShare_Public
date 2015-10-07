namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments_Update21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceChangeRequestForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PriceChangeRequestForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.PriceChangeRequestForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.OrderCancellationForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderCancellationForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.OrderCancellationForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.HotRushForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.HotRushForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.HotRushForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.LowInventoryForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LowInventoryForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.LowInventoryForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.SampleRequestForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SampleRequestForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.SampleRequestForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.DirectOrderForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DirectOrderForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.DirectOrderForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.RequestForCheckForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestForCheckForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.RequestForCheckForms", "AssignedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.MustIncludeForms", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MustIncludeForms", "SubmittedUser_SystemUserID", c => c.Int());
            AddColumn("dbo.MustIncludeForms", "AssignedUser_SystemUserID", c => c.Int());
            AddForeignKey("dbo.PriceChangeRequestForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.PriceChangeRequestForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.OrderCancellationForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.OrderCancellationForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.HotRushForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.HotRushForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.LowInventoryForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.LowInventoryForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.SampleRequestForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.SampleRequestForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.DirectOrderForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.DirectOrderForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.RequestForCheckForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.RequestForCheckForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.MustIncludeForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            AddForeignKey("dbo.MustIncludeForms", "AssignedUser_SystemUserID", "dbo.SystemUsers", "SystemUserID");
            CreateIndex("dbo.PriceChangeRequestForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.PriceChangeRequestForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.OrderCancellationForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.OrderCancellationForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.HotRushForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.HotRushForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.LowInventoryForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.LowInventoryForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.SampleRequestForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.SampleRequestForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.DirectOrderForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.DirectOrderForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.RequestForCheckForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.RequestForCheckForms", "AssignedUser_SystemUserID");
            CreateIndex("dbo.MustIncludeForms", "SubmittedUser_SystemUserID");
            CreateIndex("dbo.MustIncludeForms", "AssignedUser_SystemUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MustIncludeForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.MustIncludeForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.RequestForCheckForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.RequestForCheckForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.DirectOrderForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.DirectOrderForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.SampleRequestForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.SampleRequestForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.LowInventoryForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.LowInventoryForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.HotRushForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.HotRushForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.OrderCancellationForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.OrderCancellationForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.PriceChangeRequestForms", new[] { "AssignedUser_SystemUserID" });
            DropIndex("dbo.PriceChangeRequestForms", new[] { "SubmittedUser_SystemUserID" });
            DropForeignKey("dbo.MustIncludeForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.MustIncludeForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.RequestForCheckForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.RequestForCheckForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.DirectOrderForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.DirectOrderForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.SampleRequestForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.SampleRequestForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.LowInventoryForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.LowInventoryForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.HotRushForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.HotRushForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.OrderCancellationForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.OrderCancellationForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.PriceChangeRequestForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.PriceChangeRequestForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropColumn("dbo.MustIncludeForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.MustIncludeForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.MustIncludeForms", "DueDate");
            DropColumn("dbo.RequestForCheckForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.RequestForCheckForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.RequestForCheckForms", "DueDate");
            DropColumn("dbo.DirectOrderForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.DirectOrderForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.DirectOrderForms", "DueDate");
            DropColumn("dbo.SampleRequestForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.SampleRequestForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.SampleRequestForms", "DueDate");
            DropColumn("dbo.LowInventoryForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.LowInventoryForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.LowInventoryForms", "DueDate");
            DropColumn("dbo.HotRushForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.HotRushForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.HotRushForms", "DueDate");
            DropColumn("dbo.OrderCancellationForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.OrderCancellationForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.OrderCancellationForms", "DueDate");
            DropColumn("dbo.PriceChangeRequestForms", "AssignedUser_SystemUserID");
            DropColumn("dbo.PriceChangeRequestForms", "SubmittedUser_SystemUserID");
            DropColumn("dbo.PriceChangeRequestForms", "DueDate");
        }
    }
}
