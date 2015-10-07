namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductModelToPriceChangeRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceChangeRequestForms", "Product_RecordId", c => c.Int());
            AddForeignKey("dbo.PriceChangeRequestForms", "Product_RecordId", "dbo.PriceChangeRequestProducts", "RecordId");
            CreateIndex("dbo.PriceChangeRequestForms", "Product_RecordId");
            DropColumn("dbo.PriceChangeRequestForms", "Product");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceChangeRequestForms", "Product", c => c.String(maxLength: 100));
            DropIndex("dbo.PriceChangeRequestForms", new[] { "Product_RecordId" });
            DropForeignKey("dbo.PriceChangeRequestForms", "Product_RecordId", "dbo.PriceChangeRequestProducts");
            DropColumn("dbo.PriceChangeRequestForms", "Product_RecordId");
        }
    }
}
