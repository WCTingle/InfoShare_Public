namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTempIdToSkuQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SkuQuantities", "TempId", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SkuQuantities", "TempId");
        }
    }
}
