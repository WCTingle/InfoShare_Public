namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments_Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "FormId", "dbo.TForms");
            DropIndex("dbo.Comments", new[] { "FormId" });
            RenameColumn(table: "dbo.Comments", name: "FormId", newName: "Form_FormID");
            AddColumn("dbo.Comments", "RelatedFormId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Comments", "Form_FormID", "dbo.TForms", "FormID");
            CreateIndex("dbo.Comments", "Form_FormID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "Form_FormID" });
            DropForeignKey("dbo.Comments", "Form_FormID", "dbo.TForms");
            DropColumn("dbo.Comments", "RelatedFormId");
            RenameColumn(table: "dbo.Comments", name: "Form_FormID", newName: "FormId");
            CreateIndex("dbo.Comments", "FormId");
            AddForeignKey("dbo.Comments", "FormId", "dbo.TForms", "FormID", cascadeDelete: true);
        }
    }
}
