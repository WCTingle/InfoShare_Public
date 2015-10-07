namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserStatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserStatus",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        StatusText = c.String(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            AddColumn("dbo.SystemUsers", "UserStatus_RecordId", c => c.Int());
            CreateIndex("dbo.SystemUsers", "UserStatus_RecordId");
            AddForeignKey("dbo.SystemUsers", "UserStatus_RecordId", "dbo.UserStatus", "RecordId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemUsers", "UserStatus_RecordId", "dbo.UserStatus");
            DropIndex("dbo.SystemUsers", new[] { "UserStatus_RecordId" });
            DropColumn("dbo.SystemUsers", "UserStatus_RecordId");
            DropTable("dbo.UserStatus");
        }
    }
}
