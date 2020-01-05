namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update002 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pharmacies", "NIP", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pharmacies", "NIP", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
