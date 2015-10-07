namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestHandler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceChangeRequestForms", "RequestHandler", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceChangeRequestForms", "RequestHandler");
        }
    }
}
