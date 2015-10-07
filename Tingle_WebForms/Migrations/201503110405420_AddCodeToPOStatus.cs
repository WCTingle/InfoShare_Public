namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodeToPOStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderStatus", "StatusCode", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrderStatus", "StatusCode");
        }
    }
}
