namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShipToPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "ShipToPhone", c => c.String(maxLength: 15));
            AddColumn("dbo.SampleRequestForms", "ShipToPhone", c => c.String(maxLength: 15));
            AddColumn("dbo.DirectOrderForms", "ShipToPhone", c => c.String(maxLength: 15));
            AddColumn("dbo.RequestForCheckForms", "ShipToPhone", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestForCheckForms", "ShipToPhone");
            DropColumn("dbo.DirectOrderForms", "ShipToPhone");
            DropColumn("dbo.SampleRequestForms", "ShipToPhone");
            DropColumn("dbo.ExpeditedOrderForms", "ShipToPhone");
        }
    }
}
