namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPlantIssue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plant",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        PlantText = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.RecordId);
            
            DropTable("dbo.Warehouses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        WarehouseText = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.RecordId);
            
            DropTable("dbo.Plant");
        }
    }
}
