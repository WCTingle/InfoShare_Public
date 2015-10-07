namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeQuantityToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LowInventoryForms", "Quantity", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LowInventoryForms", "Quantity", c => c.Int(nullable: false));
        }
    }
}
