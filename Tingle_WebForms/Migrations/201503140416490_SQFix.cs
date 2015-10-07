namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DirectOrderForms", "MaterialSku");
            DropColumn("dbo.DirectOrderForms", "QuantityOrdered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DirectOrderForms", "QuantityOrdered", c => c.String());
            AddColumn("dbo.DirectOrderForms", "MaterialSku", c => c.String(maxLength: 100));
        }
    }
}
