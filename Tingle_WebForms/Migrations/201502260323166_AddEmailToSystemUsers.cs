namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToSystemUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemUsers", "EmailAddress", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemUsers", "EmailAddress");
        }
    }
}
