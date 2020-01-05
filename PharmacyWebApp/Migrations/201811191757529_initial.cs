namespace PharmacyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PostCode = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HousingNumber = c.Int(nullable: false),
                        FlatNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TotalCost = c.String(),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.OrderedMedicines",
                c => new
                    {
                        OrderedMedicineId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        PriceId = c.Int(nullable: false),
                        OrdersNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderedMedicineId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Prices", t => t.PriceId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.PriceId);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        PharmacyId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("dbo.Pharmacies", t => t.PharmacyId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.PharmacyId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Form = c.String(),
                        PregnancyUseable = c.Boolean(nullable: false),
                        PrescriptionRequired = c.Boolean(nullable: false),
                        AplicationMethod = c.String(),
                        Size = c.String(),
                        Actions = c.String(),
                        Recommendations = c.String(),
                        PharmacyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicineId)
                .ForeignKey("dbo.Pharmacies", t => t.PharmacyId, cascadeDelete: false)
                .Index(t => t.PharmacyId);
            
            CreateTable(
                "dbo.AgeCategories",
                c => new
                    {
                        AgeCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgeCategoryId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Concentrations",
                c => new
                    {
                        ConcentrationId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        ComponentId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConcentrationId)
                .ForeignKey("dbo.Components", t => t.ComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.ComponentId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        ComponentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ComponentId);
            
            CreateTable(
                "dbo.Pharmacies",
                c => new
                    {
                        PharmacyId = c.Int(nullable: false, identity: true),
                        PharmacyKey = c.String(),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        NIP = c.Int(nullable: false),
                        PostCode = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        HousingNumber = c.Int(nullable: false),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PharmacyId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ProvinceId);
            
            CreateTable(
                "dbo.WorkingHours",
                c => new
                    {
                        WorkingHourId = c.Int(nullable: false),
                        MondayFrom = c.String(),
                        MondayTo = c.String(),
                        TuesdayFrom = c.String(),
                        TuesdayTo = c.String(),
                        WednesdayFrom = c.String(),
                        WednesdayTo = c.String(),
                        ThursdayFrom = c.String(),
                        ThursdayTo = c.String(),
                        FridayFrom = c.String(),
                        FridayTo = c.String(),
                        SaturdayFrom = c.String(),
                        SaturdayTo = c.String(),
                        SundayFrom = c.String(),
                        SundayTo = c.String(),
                    })
                .PrimaryKey(t => t.WorkingHourId)
                .ForeignKey("dbo.Pharmacies", t => t.WorkingHourId)
                .Index(t => t.WorkingHourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedMedicines", "PriceId", "dbo.Prices");
            DropForeignKey("dbo.Prices", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.WorkingHours", "WorkingHourId", "dbo.Pharmacies");
            DropForeignKey("dbo.Prices", "PharmacyId", "dbo.Pharmacies");
            DropForeignKey("dbo.Pharmacies", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Medicines", "PharmacyId", "dbo.Pharmacies");
            DropForeignKey("dbo.Concentrations", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.Concentrations", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.AgeCategories", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.OrderedMedicines", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AccountId", "dbo.Accounts");
            DropIndex("dbo.WorkingHours", new[] { "WorkingHourId" });
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.Pharmacies", new[] { "CityId" });
            DropIndex("dbo.Concentrations", new[] { "MedicineId" });
            DropIndex("dbo.Concentrations", new[] { "ComponentId" });
            DropIndex("dbo.AgeCategories", new[] { "MedicineId" });
            DropIndex("dbo.Medicines", new[] { "PharmacyId" });
            DropIndex("dbo.Prices", new[] { "MedicineId" });
            DropIndex("dbo.Prices", new[] { "PharmacyId" });
            DropIndex("dbo.OrderedMedicines", new[] { "PriceId" });
            DropIndex("dbo.OrderedMedicines", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "AccountId" });
            DropTable("dbo.WorkingHours");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.Pharmacies");
            DropTable("dbo.Components");
            DropTable("dbo.Concentrations");
            DropTable("dbo.AgeCategories");
            DropTable("dbo.Medicines");
            DropTable("dbo.Prices");
            DropTable("dbo.OrderedMedicines");
            DropTable("dbo.Orders");
            DropTable("dbo.Accounts");
        }
    }
}
