namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarehouses_MustInclude : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        WarehouseText = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.RecordId);
            
            AddColumn("dbo.MustIncludeForms", "Warehouse_RecordId", c => c.Int());
            AddForeignKey("dbo.MustIncludeForms", "Warehouse_RecordId", "dbo.Warehouses", "RecordId");
            CreateIndex("dbo.MustIncludeForms", "Warehouse_RecordId");
            DropColumn("dbo.MustIncludeForms", "Warehouse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MustIncludeForms", "Warehouse", c => c.String(maxLength: 100));
            DropIndex("dbo.MustIncludeForms", new[] { "Warehouse_RecordId" });
            DropForeignKey("dbo.MustIncludeForms", "Warehouse_RecordId", "dbo.Warehouses");
            DropColumn("dbo.MustIncludeForms", "Warehouse_RecordId");
            DropTable("dbo.Warehouses");
        }
    }
}
