namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkuQuantityModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkuQuantities",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        MaterialSku = c.String(maxLength: 100),
                        Quantity = c.String(maxLength: 100),
                        ExpeditedOrderForm_RecordId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.ExpeditedOrderForms", t => t.ExpeditedOrderForm_RecordId)
                .Index(t => t.ExpeditedOrderForm_RecordId);
            
            DropColumn("dbo.ExpeditedOrderForms", "MaterialSku");
            DropColumn("dbo.ExpeditedOrderForms", "QuantityOrdered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExpeditedOrderForms", "QuantityOrdered", c => c.String());
            AddColumn("dbo.ExpeditedOrderForms", "MaterialSku", c => c.String(maxLength: 100));
            DropIndex("dbo.SkuQuantities", new[] { "ExpeditedOrderForm_RecordId" });
            DropForeignKey("dbo.SkuQuantities", "ExpeditedOrderForm_RecordId", "dbo.ExpeditedOrderForms");
            DropTable("dbo.SkuQuantities");
        }
    }
}
