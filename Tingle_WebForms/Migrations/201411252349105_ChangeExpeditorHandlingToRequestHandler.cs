namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeExpeditorHandlingToRequestHandler : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ExpeditedOrderForms", "ExpeditorHandling", "RequestHandler");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.ExpeditedOrderForms", "RequestHandler", "ExpeditorHandling");
        }
    }
}
