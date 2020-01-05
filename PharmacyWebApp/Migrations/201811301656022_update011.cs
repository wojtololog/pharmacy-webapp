namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "AvailableWithoutPrescription", c => c.Boolean(nullable: false));
            DropColumn("dbo.Medicines", "PrescriptionRequired");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicines", "PrescriptionRequired", c => c.Boolean(nullable: false));
            DropColumn("dbo.Medicines", "AvailableWithoutPrescription");
        }
    }
}
