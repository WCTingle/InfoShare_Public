namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlantToLowInventoryForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LowInventoryForms", "Plant_RecordId", c => c.Int());
            AddForeignKey("dbo.LowInventoryForms", "Plant_RecordId", "dbo.Plant", "RecordId");
            CreateIndex("dbo.LowInventoryForms", "Plant_RecordId");
            DropColumn("dbo.LowInventoryForms", "Plant");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LowInventoryForms", "Plant", c => c.String(maxLength: 100));
            DropIndex("dbo.LowInventoryForms", new[] { "Plant_RecordId" });
            DropForeignKey("dbo.LowInventoryForms", "Plant_RecordId", "dbo.Plant");
            DropColumn("dbo.LowInventoryForms", "Plant_RecordId");
        }
    }
}
