namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectOrder4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestNotifications", "IncludeComments", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestNotifications", "IncludeComments");
        }
    }
}
