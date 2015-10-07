namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectOrder3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DirectOrderForms", "Priority", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DirectOrderForms", "Priority");
        }
    }
}
