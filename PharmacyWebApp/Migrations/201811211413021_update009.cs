namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update009 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AgeCategories", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.AgeCategories", new[] { "MedicineId" });
            CreateTable(
                "dbo.AgeCategoryMedicines",
                c => new
                    {
                        AgeCategory_AgeCategoryId = c.Int(nullable: false),
                        Medicine_MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AgeCategory_AgeCategoryId, t.Medicine_MedicineId })
                .ForeignKey("dbo.AgeCategories", t => t.AgeCategory_AgeCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineId, cascadeDelete: true)
                .Index(t => t.AgeCategory_AgeCategoryId)
                .Index(t => t.Medicine_MedicineId);
            
            DropColumn("dbo.AgeCategories", "MedicineId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AgeCategories", "MedicineId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AgeCategoryMedicines", "Medicine_MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.AgeCategoryMedicines", "AgeCategory_AgeCategoryId", "dbo.AgeCategories");
            DropIndex("dbo.AgeCategoryMedicines", new[] { "Medicine_MedicineId" });
            DropIndex("dbo.AgeCategoryMedicines", new[] { "AgeCategory_AgeCategoryId" });
            DropTable("dbo.AgeCategoryMedicines");
            CreateIndex("dbo.AgeCategories", "MedicineId");
            AddForeignKey("dbo.AgeCategories", "MedicineId", "dbo.Medicines", "MedicineId", cascadeDelete: true);
        }
    }
}
