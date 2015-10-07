namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompletedNotesAndCCCompletedFormToEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "CompletedNotes", c => c.String(maxLength: 4000));
            AddColumn("dbo.ExpeditedOrderForms", "CCCompletedFormToEmail", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpeditedOrderForms", "CCCompletedFormToEmail");
            DropColumn("dbo.ExpeditedOrderForms", "CompletedNotes");
        }
    }
}
