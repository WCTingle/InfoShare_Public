namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CannotWaitForContainerModel_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CannotWaitForContainerForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        OrderNumber = c.String(maxLength: 100),
                        Line = c.String(maxLength: 100),
                        Quantity = c.String(maxLength: 100),
                        SKU = c.String(maxLength: 100),
                        AdditionalInfo = c.String(maxLength: 2000),
                        RequestHandler = c.String(maxLength: 100),
                        Company = c.String(maxLength: 10),
                        DueDate = c.DateTime(),
                        LastModifiedTimestamp = c.DateTime(nullable: false),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        CCFormToEmail = c.String(maxLength: 1000),
                        CompletedNotes = c.String(maxLength: 4000),
                        CCCompletedFormToEmail = c.String(maxLength: 1000),
                        AssignedUser_SystemUserID = c.Int(),
                        LastModifiedUser_SystemUserID = c.Int(),
                        Plant_RecordId = c.Int(),
                        Priority_RecordId = c.Int(),
                        RequestedUser_SystemUserID = c.Int(),
                        Status_StatusId = c.Int(),
                        SubmittedUser_SystemUserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.AssignedUser_SystemUserID)
                .ForeignKey("dbo.SystemUsers", t => t.LastModifiedUser_SystemUserID)
                .ForeignKey("dbo.Plant", t => t.Plant_RecordId)
                .ForeignKey("dbo.Priorities", t => t.Priority_RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.RequestedUser_SystemUserID)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .ForeignKey("dbo.SystemUsers", t => t.SubmittedUser_SystemUserID)
                .Index(t => t.AssignedUser_SystemUserID)
                .Index(t => t.LastModifiedUser_SystemUserID)
                .Index(t => t.Plant_RecordId)
                .Index(t => t.Priority_RecordId)
                .Index(t => t.RequestedUser_SystemUserID)
                .Index(t => t.Status_StatusId)
                .Index(t => t.SubmittedUser_SystemUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CannotWaitForContainerForms", "SubmittedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.CannotWaitForContainerForms", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.CannotWaitForContainerForms", "RequestedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.CannotWaitForContainerForms", "Priority_RecordId", "dbo.Priorities");
            DropForeignKey("dbo.CannotWaitForContainerForms", "Plant_RecordId", "dbo.Plant");
            DropForeignKey("dbo.CannotWaitForContainerForms", "LastModifiedUser_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.CannotWaitForContainerForms", "AssignedUser_SystemUserID", "dbo.SystemUsers");
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "SubmittedUser_SystemUserID" });
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "Status_StatusId" });
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "RequestedUser_SystemUserID" });
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "Priority_RecordId" });
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "Plant_RecordId" });
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "LastModifiedUser_SystemUserID" });
            DropIndex("dbo.CannotWaitForContainerForms", new[] { "AssignedUser_SystemUserID" });
            DropTable("dbo.CannotWaitForContainerForms");
        }
    }
}
