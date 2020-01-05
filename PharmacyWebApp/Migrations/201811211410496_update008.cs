namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update008 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActionMedicines", "Action_ActionId", "dbo.Actions");
            DropForeignKey("dbo.ActionMedicines", "Medicine_MedicineId", "dbo.Medicines");
            DropIndex("dbo.ActionMedicines", new[] { "Action_ActionId" });
            DropIndex("dbo.ActionMedicines", new[] { "Medicine_MedicineId" });
            CreateTable(
                "dbo.Problems",
                c => new
                    {
                        ProblemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProblemId);
            
            CreateTable(
                "dbo.ProblemMedicines",
                c => new
                    {
                        Problem_ProblemId = c.Int(nullable: false),
                        Medicine_MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Problem_ProblemId, t.Medicine_MedicineId })
                .ForeignKey("dbo.Problems", t => t.Problem_ProblemId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineId, cascadeDelete: true)
                .Index(t => t.Problem_ProblemId)
                .Index(t => t.Medicine_MedicineId);
            
            DropTable("dbo.Actions");
            DropTable("dbo.ActionMedicines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ActionMedicines",
                c => new
                    {
                        Action_ActionId = c.Int(nullable: false),
                        Medicine_MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Action_ActionId, t.Medicine_MedicineId });
            
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ActionId);
            
            DropForeignKey("dbo.ProblemMedicines", "Medicine_MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.ProblemMedicines", "Problem_ProblemId", "dbo.Problems");
            DropIndex("dbo.ProblemMedicines", new[] { "Medicine_MedicineId" });
            DropIndex("dbo.ProblemMedicines", new[] { "Problem_ProblemId" });
            DropTable("dbo.ProblemMedicines");
            DropTable("dbo.Problems");
            CreateIndex("dbo.ActionMedicines", "Medicine_MedicineId");
            CreateIndex("dbo.ActionMedicines", "Action_ActionId");
            AddForeignKey("dbo.ActionMedicines", "Medicine_MedicineId", "dbo.Medicines", "MedicineId", cascadeDelete: true);
            AddForeignKey("dbo.ActionMedicines", "Action_ActionId", "dbo.Actions", "ActionId", cascadeDelete: true);
        }
    }
}
