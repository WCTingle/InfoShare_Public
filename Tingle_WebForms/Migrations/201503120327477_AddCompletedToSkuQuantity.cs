namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompletedToSkuQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SkuQuantities", "Completed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SkuQuantities", "Completed");
        }
    }
}
