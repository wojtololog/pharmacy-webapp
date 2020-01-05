namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update010 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medicines", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medicines", "Name", c => c.String());
        }
    }
}
