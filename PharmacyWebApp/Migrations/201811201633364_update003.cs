namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pharmacies", "RegistrationConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pharmacies", "RegistrationConfirmed");
        }
    }
}
