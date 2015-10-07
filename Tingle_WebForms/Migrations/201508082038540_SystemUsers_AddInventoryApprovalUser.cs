namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SystemUsers_AddInventoryApprovalUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemUsers", "InventoryApprovalUser", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemUsers", "InventoryApprovalUser");
        }
    }
}
