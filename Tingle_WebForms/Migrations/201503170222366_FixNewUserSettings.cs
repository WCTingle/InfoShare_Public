namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNewUserSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemUsers", "Greeting", c => c.String(maxLength: 50));
            AddColumn("dbo.SystemUsers", "Points", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemUsers", "Points");
            DropColumn("dbo.SystemUsers", "Greeting");
        }
    }
}
