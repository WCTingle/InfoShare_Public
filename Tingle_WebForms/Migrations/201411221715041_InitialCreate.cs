namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpeditedOrderForms",
                c => new
                    {
                        ExpeditedOrderFormID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        OowOrderNumber = c.String(maxLength: 100),
                        Customer = c.String(maxLength: 100),
                        AccountNumber = c.String(maxLength: 6),
                        PurchaseOrderNumber = c.String(maxLength: 100),
                        MaterialSku = c.String(maxLength: 100),
                        QuantityOrdered = c.String(),
                        InstallDate = c.DateTime(),
                        SM = c.String(maxLength: 100),
                        ContactName = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 100),
                        ShipToName = c.String(maxLength: 100),
                        ShipToAddress = c.String(maxLength: 100),
                        ShipToCity = c.String(maxLength: 100),
                        ShipToState = c.String(maxLength: 100),
                        ShipToZip = c.String(maxLength: 100),
                        AdditionalInfo = c.String(maxLength: 2000),
                        ExpeditorHandling = c.String(maxLength: 100),
                        SubmittedByUser = c.String(maxLength: 100),
                        ModifiedByUser = c.String(maxLength: 100),
                        CCFormToEmail = c.String(maxLength: 100),
                        ExpediteCode_ExpediteCodeID = c.Int(nullable: false),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpeditedOrderFormID)
                .ForeignKey("dbo.ExpediteCodes", t => t.ExpediteCode_ExpediteCodeID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.ExpediteCode_ExpediteCodeID)
                .Index(t => t.Status_StatusId);
            
            CreateTable(
                "dbo.ExpediteCodes",
                c => new
                    {
                        ExpediteCodeID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ExpediteCodeID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusText = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.TForms",
                c => new
                    {
                        FormID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        FormName = c.String(maxLength: 200),
                        FormNameHtml = c.String(maxLength: 400),
                        FormCreator = c.String(maxLength: 200),
                        Notes = c.String(maxLength: 200),
                        FormUrl = c.String(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.FormID);
            
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        EmailAddressID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Address = c.String(),
                        Name = c.String(),
                        Status = c.Short(nullable: false),
                        TForm_FormID = c.Int(),
                    })
                .PrimaryKey(t => t.EmailAddressID)
                .ForeignKey("dbo.TForms", t => t.TForm_FormID)
                .Index(t => t.TForm_FormID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleDescription = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        SystemUserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        DisplayName = c.String(),
                        Status = c.Short(nullable: false),
                        UserRole_UserRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.SystemUserID)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_UserRoleId)
                .Index(t => t.UserRole_UserRoleId);
            
            CreateTable(
                "dbo.FavoriteForms",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        User_SystemUserID = c.Int(),
                        Form_FormID = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.SystemUsers", t => t.User_SystemUserID)
                .ForeignKey("dbo.TForms", t => t.Form_FormID)
                .Index(t => t.User_SystemUserID)
                .Index(t => t.Form_FormID);
        }
        
        public override void Down()
        {
            DropIndex("dbo.FavoriteForms", new[] { "Form_FormID" });
            DropIndex("dbo.FavoriteForms", new[] { "User_SystemUserID" });
            DropIndex("dbo.SystemUsers", new[] { "UserRole_UserRoleId" });
            DropIndex("dbo.EmailAddresses", new[] { "TForm_FormID" });
            DropIndex("dbo.ExpeditedOrderForms", new[] { "Status_StatusId" });
            DropIndex("dbo.ExpeditedOrderForms", new[] { "ExpediteCode_ExpediteCodeID" });
            DropForeignKey("dbo.FavoriteForms", "Form_FormID", "dbo.TForms");
            DropForeignKey("dbo.FavoriteForms", "User_SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.SystemUsers", "UserRole_UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.EmailAddresses", "TForm_FormID", "dbo.TForms");
            DropForeignKey("dbo.ExpeditedOrderForms", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.ExpeditedOrderForms", "ExpediteCode_ExpediteCodeID", "dbo.ExpediteCodes");
            DropTable("dbo.FavoriteForms");
            DropTable("dbo.SystemUsers");
            DropTable("dbo.UserRoles");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.TForms");
            DropTable("dbo.Status");
            DropTable("dbo.ExpediteCodes");
            DropTable("dbo.ExpeditedOrderForms");
        }
    }
}
