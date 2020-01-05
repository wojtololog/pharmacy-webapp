namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update017 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prices", "Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prices", "Value", c => c.Single(nullable: false));
        }
    }
}
