namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prices", "Size", c => c.String());
            DropColumn("dbo.Medicines", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicines", "Size", c => c.String());
            DropColumn("dbo.Prices", "Size");
        }
    }
}
