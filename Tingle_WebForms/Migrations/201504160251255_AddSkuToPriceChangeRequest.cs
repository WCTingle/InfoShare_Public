namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkuToPriceChangeRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceChangeRequestForms", "SKU", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceChangeRequestForms", "SKU");
        }
    }
}
