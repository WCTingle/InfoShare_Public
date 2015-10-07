namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryApprovalComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryComments",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Note = c.String(maxLength: 300),
                        SystemComment = c.Boolean(nullable: false),
                        User_SystemUserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.User_SystemUserID)
                .Index(t => t.User_SystemUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryComments", "User_SystemUserID", "dbo.SystemUsers");
            DropIndex("dbo.InventoryComments", new[] { "User_SystemUserID" });
            DropTable("dbo.InventoryComments");
        }
    }
}
