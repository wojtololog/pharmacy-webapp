namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update004 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ActionId);
            
            CreateTable(
                "dbo.ActionMedicines",
                c => new
                    {
                        Action_ActionId = c.Int(nullable: false),
                        Medicine_MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Action_ActionId, t.Medicine_MedicineId })
                .ForeignKey("dbo.Actions", t => t.Action_ActionId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineId, cascadeDelete: true)
                .Index(t => t.Action_ActionId)
                .Index(t => t.Medicine_MedicineId);
            
            DropColumn("dbo.Medicines", "Actions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicines", "Actions", c => c.String());
            DropForeignKey("dbo.ActionMedicines", "Medicine_MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.ActionMedicines", "Action_ActionId", "dbo.Actions");
            DropIndex("dbo.ActionMedicines", new[] { "Medicine_MedicineId" });
            DropIndex("dbo.ActionMedicines", new[] { "Action_ActionId" });
            DropIndex("dbo.Pharmacies", new[] { "NIP" });
            DropIndex("dbo.Pharmacies", new[] { "Email" });
            DropTable("dbo.ActionMedicines");
            DropTable("dbo.Actions");
        }
    }
}
