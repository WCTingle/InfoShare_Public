namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryApproval_ChangeCostToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryApprovalForms", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryApprovalForms", "Cost", c => c.String());
        }
    }
}
