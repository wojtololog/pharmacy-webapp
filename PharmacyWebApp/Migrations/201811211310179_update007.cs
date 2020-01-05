namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update007 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AplicationMethods",
                c => new
                    {
                        AplicationMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AplicationMethodId);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        FormId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FormId);
            
            AddColumn("dbo.Medicines", "FormId", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "AplicationMethodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicines", "FormId");
            CreateIndex("dbo.Medicines", "AplicationMethodId");
            AddForeignKey("dbo.Medicines", "AplicationMethodId", "dbo.AplicationMethods", "AplicationMethodId", cascadeDelete: true);
            AddForeignKey("dbo.Medicines", "FormId", "dbo.Forms", "FormId", cascadeDelete: true);
            DropColumn("dbo.Medicines", "Form");
            DropColumn("dbo.Medicines", "AplicationMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicines", "AplicationMethod", c => c.String());
            AddColumn("dbo.Medicines", "Form", c => c.String());
            DropForeignKey("dbo.Medicines", "FormId", "dbo.Forms");
            DropForeignKey("dbo.Medicines", "AplicationMethodId", "dbo.AplicationMethods");
            DropIndex("dbo.Medicines", new[] { "AplicationMethodId" });
            DropIndex("dbo.Medicines", new[] { "FormId" });
            DropColumn("dbo.Medicines", "AplicationMethodId");
            DropColumn("dbo.Medicines", "FormId");
            DropTable("dbo.Forms");
            DropTable("dbo.AplicationMethods");
        }
    }
}
