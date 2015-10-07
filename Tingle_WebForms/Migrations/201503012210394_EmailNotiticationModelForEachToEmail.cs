namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailNotiticationModelForEachToEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestNotifications", "ToEmailAddress", c => c.String());
            AddColumn("dbo.RequestNotifications", "Status", c => c.Short(nullable: false));
            DropColumn("dbo.RequestNotifications", "EmailList");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestNotifications", "EmailList", c => c.String());
            DropColumn("dbo.RequestNotifications", "Status");
            DropColumn("dbo.RequestNotifications", "ToEmailAddress");
        }
    }
}
