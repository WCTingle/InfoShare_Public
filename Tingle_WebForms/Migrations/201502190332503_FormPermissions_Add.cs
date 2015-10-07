namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormPermissions_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormPermissions",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        FormName = c.String(maxLength: 100),
                        Enabled = c.Boolean(nullable: false),
                        UserRole_UserRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_UserRoleId)
                .Index(t => t.UserRole_UserRoleId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.FormPermissions", new[] { "UserRole_UserRoleId" });
            DropForeignKey("dbo.FormPermissions", "UserRole_UserRoleId", "dbo.UserRoles");
            DropTable("dbo.FormPermissions");
        }
    }
}
