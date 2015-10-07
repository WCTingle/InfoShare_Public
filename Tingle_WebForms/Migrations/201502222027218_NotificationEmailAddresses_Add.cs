namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationEmailAddresses_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationEmailAddresses",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NotificationEmailAddresses");
        }
    }
}
