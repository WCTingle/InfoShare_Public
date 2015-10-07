namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vendor_AddCurrentStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendors", "CurrentStock", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendors", "CurrentStock");
        }
    }
}
