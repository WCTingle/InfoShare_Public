namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentLength200 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Note", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Note", c => c.String(maxLength: 180));
        }
    }
}
