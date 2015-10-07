namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBodyHtmlToRequestNotifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestNotifications", "BodyHtml", c => c.String());
            DropColumn("dbo.RequestNotifications", "IncludeComments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestNotifications", "IncludeComments", c => c.Boolean(nullable: false));
            DropColumn("dbo.RequestNotifications", "BodyHtml");
        }
    }
}
