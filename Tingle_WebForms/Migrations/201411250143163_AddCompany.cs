namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpeditedOrderForms", "Company", c => c.String(maxLength: 10));
            AddColumn("dbo.PriceChangeRequestForms", "Company", c => c.String(maxLength: 10));
            AddColumn("dbo.OrderCancellations", "Company", c => c.String(maxLength: 10));
            AddColumn("dbo.EmailAddresses", "Company", c => c.String(maxLength: 10));
            AlterColumn("dbo.ExpediteCodes", "Code", c => c.String(maxLength: 20));
            AlterColumn("dbo.ExpediteCodes", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.Status", "StatusText", c => c.String(maxLength: 20));
            AlterColumn("dbo.PurchaseOrderStatus", "Status", c => c.String(maxLength: 20));
            AlterColumn("dbo.TForms", "FormUrl", c => c.String(maxLength: 200));
            AlterColumn("dbo.EmailAddresses", "Address", c => c.String(maxLength: 100));
            AlterColumn("dbo.EmailAddresses", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.UserRoles", "RoleName", c => c.String(maxLength: 100));
            AlterColumn("dbo.UserRoles", "RoleDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.SystemUsers", "UserName", c => c.String(maxLength: 50));
            AlterColumn("dbo.SystemUsers", "DisplayName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SystemUsers", "DisplayName", c => c.String());
            AlterColumn("dbo.SystemUsers", "UserName", c => c.String());
            AlterColumn("dbo.UserRoles", "RoleDescription", c => c.String());
            AlterColumn("dbo.UserRoles", "RoleName", c => c.String());
            AlterColumn("dbo.EmailAddresses", "Name", c => c.String());
            AlterColumn("dbo.EmailAddresses", "Address", c => c.String());
            AlterColumn("dbo.TForms", "FormUrl", c => c.String());
            AlterColumn("dbo.PurchaseOrderStatus", "Status", c => c.String());
            AlterColumn("dbo.Status", "StatusText", c => c.String());
            AlterColumn("dbo.ExpediteCodes", "Description", c => c.String());
            AlterColumn("dbo.ExpediteCodes", "Code", c => c.String());
            DropColumn("dbo.EmailAddresses", "Company");
            DropColumn("dbo.OrderCancellations", "Company");
            DropColumn("dbo.PriceChangeRequestForms", "Company");
            DropColumn("dbo.ExpeditedOrderForms", "Company");
        }
    }
}
