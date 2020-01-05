namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update005 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pharmacies", new[] { "Email" });
            DropIndex("dbo.Pharmacies", new[] { "NIP" });
            AlterColumn("dbo.Concentrations", "Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Concentrations", "Value", c => c.Single(nullable: false));
            CreateIndex("dbo.Pharmacies", "NIP", unique: true);
            CreateIndex("dbo.Pharmacies", "Email", unique: true);
        }
    }
}
