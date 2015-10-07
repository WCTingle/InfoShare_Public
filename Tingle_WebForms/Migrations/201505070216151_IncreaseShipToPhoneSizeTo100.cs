namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseShipToPhoneSizeTo100 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DirectOrderForms", "ShipToPhone", c => c.String(maxLength: 100));
            AlterColumn("dbo.ExpeditedOrderForms", "ShipToPhone", c => c.String(maxLength: 100));
            AlterColumn("dbo.RequestForCheckForms", "ShipToPhone", c => c.String(maxLength: 100));
            AlterColumn("dbo.SampleRequestForms", "ShipToPhone", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SampleRequestForms", "ShipToPhone", c => c.String(maxLength: 15));
            AlterColumn("dbo.RequestForCheckForms", "ShipToPhone", c => c.String(maxLength: 15));
            AlterColumn("dbo.ExpeditedOrderForms", "ShipToPhone", c => c.String(maxLength: 15));
            AlterColumn("dbo.DirectOrderForms", "ShipToPhone", c => c.String(maxLength: 15));
        }
    }
}
