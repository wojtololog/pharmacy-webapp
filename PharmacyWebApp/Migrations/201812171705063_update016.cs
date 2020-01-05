namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update016 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyParts",
                c => new
                    {
                        BodyPartId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BodyPartId);
            
            CreateTable(
                "dbo.BodyPartMedicines",
                c => new
                    {
                        BodyPart_BodyPartId = c.Int(nullable: false),
                        Medicine_MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BodyPart_BodyPartId, t.Medicine_MedicineId })
                .ForeignKey("dbo.BodyParts", t => t.BodyPart_BodyPartId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineId, cascadeDelete: true)
                .Index(t => t.BodyPart_BodyPartId)
                .Index(t => t.Medicine_MedicineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BodyPartMedicines", "Medicine_MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.BodyPartMedicines", "BodyPart_BodyPartId", "dbo.BodyParts");
            DropIndex("dbo.BodyPartMedicines", new[] { "Medicine_MedicineId" });
            DropIndex("dbo.BodyPartMedicines", new[] { "BodyPart_BodyPartId" });
            DropTable("dbo.BodyPartMedicines");
            DropTable("dbo.BodyParts");
        }
    }
}
