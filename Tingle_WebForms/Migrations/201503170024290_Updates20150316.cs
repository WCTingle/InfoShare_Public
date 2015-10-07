namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates20150316 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemUsers", "Points", c => c.Int(nullable: false));
            AddColumn("dbo.SystemUsers", "Greeting", c => c.String(maxLength: 50));
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemUsers", "Greeting");
            DropColumn("dbo.SystemUsers", "Points");
        }
    }
}
