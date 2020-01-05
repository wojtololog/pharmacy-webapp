namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update012 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prices", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.Prices", new[] { "MedicineId" });
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        SizeId = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Unit = c.String(),
                        Image = c.Binary(),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SizeId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId);
            
            AddColumn("dbo.Prices", "SizeId", c => c.Int(nullable: false));
            AddColumn("dbo.Concentrations", "Unit", c => c.String());
            CreateIndex("dbo.Prices", "SizeId");
            AddForeignKey("dbo.Prices", "SizeId", "dbo.Sizes", "SizeId", cascadeDelete: true);
            DropColumn("dbo.Prices", "Size");
            DropColumn("dbo.Prices", "MedicineId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prices", "MedicineId", c => c.Int(nullable: false));
            AddColumn("dbo.Prices", "Size", c => c.String());
            DropForeignKey("dbo.Prices", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.Sizes", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.Sizes", new[] { "MedicineId" });
            DropIndex("dbo.Prices", new[] { "SizeId" });
            DropColumn("dbo.Concentrations", "Unit");
            DropColumn("dbo.Prices", "SizeId");
            DropTable("dbo.Sizes");
            CreateIndex("dbo.Prices", "MedicineId");
            AddForeignKey("dbo.Prices", "MedicineId", "dbo.Medicines", "MedicineId", cascadeDelete: true);
        }
    }
}
