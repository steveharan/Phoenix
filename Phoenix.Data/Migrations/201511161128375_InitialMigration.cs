namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        IdentityCard = c.String(nullable: false, maxLength: 50),
                        UniqueKey = c.Guid(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Mobile = c.String(maxLength: 10),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        SurName = c.String(nullable: false, maxLength: 30),
                        DateOfBirth = c.DateTime(nullable: false),
                        Twin = c.Boolean(nullable: false),
                        Adopted = c.Boolean(nullable: false),
                        HeightCM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightKG = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deceased = c.Boolean(nullable: false),
                        FamilyId = c.Int(nullable: false),
                        DiagnosisId = c.Int(),
                        DiagnosisSubTypeId = c.Int(),
                        EthnicityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId)
                .ForeignKey("dbo.DiagnosisSubType", t => t.DiagnosisSubTypeId)
                .ForeignKey("dbo.Family", t => t.FamilyId)
                .ForeignKey("dbo.Ethnicity", t => t.EthnicityId)
                .Index(t => t.FamilyId)
                .Index(t => t.DiagnosisId)
                .Index(t => t.DiagnosisSubTypeId)
                .Index(t => t.EthnicityId);
            
            CreateTable(
                "dbo.DiagnosisSubType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId)
                .Index(t => t.DiagnosisId);
            
            CreateTable(
                "dbo.Ethnicity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EthnicityName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Family",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstRegisteredDate = c.DateTime(nullable: false),
                        Notes = c.String(nullable: false, maxLength: 100),
                        FamilyName = c.String(nullable: false, maxLength: 30),
                        EthnicityID = c.Int(nullable: false),
                        DiagnosisId = c.Int(),
                        DiagnosisSubTypeId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId)
                .ForeignKey("dbo.DiagnosisSubType", t => t.DiagnosisSubTypeId)
                .ForeignKey("dbo.Ethnicity", t => t.EthnicityID)
                .Index(t => t.EthnicityID)
                .Index(t => t.DiagnosisId)
                .Index(t => t.DiagnosisSubTypeId);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 2000),
                        Image = c.String(),
                        GenreId = c.Int(nullable: false),
                        Director = c.String(nullable: false, maxLength: 100),
                        Writer = c.String(nullable: false, maxLength: 50),
                        Producer = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        Rating = c.Byte(nullable: false),
                        TrailerURI = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genre", t => t.GenreId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        UniqueKey = c.Guid(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movie", t => t.MovieId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Rental",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                        RentalDate = c.DateTime(nullable: false),
                        ReturnedDate = c.DateTime(),
                        Status = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stock", t => t.StockId)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        Salt = c.String(nullable: false, maxLength: 200),
                        IsLocked = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Rental", "StockId", "dbo.Stock");
            DropForeignKey("dbo.Stock", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.Movie", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Person", "EthnicityId", "dbo.Ethnicity");
            DropForeignKey("dbo.Person", "FamilyId", "dbo.Family");
            DropForeignKey("dbo.Family", "EthnicityID", "dbo.Ethnicity");
            DropForeignKey("dbo.Family", "DiagnosisSubTypeId", "dbo.DiagnosisSubType");
            DropForeignKey("dbo.Family", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Person", "DiagnosisSubTypeId", "dbo.DiagnosisSubType");
            DropForeignKey("dbo.DiagnosisSubType", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Person", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Rental", new[] { "StockId" });
            DropIndex("dbo.Stock", new[] { "MovieId" });
            DropIndex("dbo.Movie", new[] { "GenreId" });
            DropIndex("dbo.Family", new[] { "DiagnosisSubTypeId" });
            DropIndex("dbo.Family", new[] { "DiagnosisId" });
            DropIndex("dbo.Family", new[] { "EthnicityID" });
            DropIndex("dbo.DiagnosisSubType", new[] { "DiagnosisId" });
            DropIndex("dbo.Person", new[] { "EthnicityId" });
            DropIndex("dbo.Person", new[] { "DiagnosisSubTypeId" });
            DropIndex("dbo.Person", new[] { "DiagnosisId" });
            DropIndex("dbo.Person", new[] { "FamilyId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.Rental");
            DropTable("dbo.Stock");
            DropTable("dbo.Movie");
            DropTable("dbo.Genre");
            DropTable("dbo.Error");
            DropTable("dbo.Family");
            DropTable("dbo.Ethnicity");
            DropTable("dbo.DiagnosisSubType");
            DropTable("dbo.Person");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Customer");
        }
    }
}
