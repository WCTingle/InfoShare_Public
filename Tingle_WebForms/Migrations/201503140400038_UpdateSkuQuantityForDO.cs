namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSkuQuantityForDO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SkuQuantities", "DirectOrderForm_RecordId", c => c.Int());
            AddForeignKey("dbo.SkuQuantities", "DirectOrderForm_RecordId", "dbo.DirectOrderForms", "RecordId");
            CreateIndex("dbo.SkuQuantities", "DirectOrderForm_RecordId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SkuQuantities", new[] { "DirectOrderForm_RecordId" });
            DropForeignKey("dbo.SkuQuantities", "DirectOrderForm_RecordId", "dbo.DirectOrderForms");
            DropColumn("dbo.SkuQuantities", "DirectOrderForm_RecordId");
        }
    }
}
