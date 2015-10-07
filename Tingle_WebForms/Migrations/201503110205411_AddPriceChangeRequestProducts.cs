namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceChangeRequestProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceChangeRequestProducts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        ProductText = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.RecordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PriceChangeRequestProducts");
        }
    }
}
