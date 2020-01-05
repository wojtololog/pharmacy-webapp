namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update013 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sizes", "Value", c => c.String(nullable: false));
            AlterColumn("dbo.Sizes", "Unit", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sizes", "Unit", c => c.String());
            AlterColumn("dbo.Sizes", "Value", c => c.String());
        }
    }
}
