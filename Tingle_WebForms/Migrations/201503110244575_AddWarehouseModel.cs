namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarehouseModel : DbMigration
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
        }
        
        public override void Down()
        {
            DropTable("dbo.Warehouses");
        }
    }
}
