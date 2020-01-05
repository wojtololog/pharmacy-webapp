namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update015 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pharmacies", "Longitude", c => c.String(nullable: false));
            AlterColumn("dbo.Pharmacies", "Latitude", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pharmacies", "Latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Pharmacies", "Longitude", c => c.Single(nullable: false));
        }
    }
}
