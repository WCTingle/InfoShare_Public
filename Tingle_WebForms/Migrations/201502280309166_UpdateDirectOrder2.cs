namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectOrder2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DirectOrderForms", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DirectOrderForms", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
