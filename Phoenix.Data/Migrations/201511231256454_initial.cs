namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
                        NhsNumber = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        SurName = c.String(nullable: false, maxLength: 30),
                        DateOfBirth = c.DateTime(nullable: false),
                        Twin = c.Boolean(nullable: false),
                        Adopted = c.Boolean(nullable: false),
                        HeightCM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightKG = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deceased = c.Boolean(nullable: false),
                        DateDeceased = c.DateTime(nullable: false),
                        FirstRegisteredDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Notes = c.String(nullable: false),
                        FamilyId = c.Int(nullable: false),
                        DiagnosisId = c.Int(),
                        DiagnosisSubTypeId = c.Int(),
                        EthnicityId = c.Int(nullable: false),
                        Deleted = c.Boolean(),
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
                        DiagnosisID = c.Int(),
                        DiagnosisSubTypeId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                        FamilyIdentifier = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisID)
                .ForeignKey("dbo.DiagnosisSubType", t => t.DiagnosisSubTypeId)
                .ForeignKey("dbo.Ethnicity", t => t.EthnicityID)
                .Index(t => t.EthnicityID)
                .Index(t => t.DiagnosisID)
                .Index(t => t.DiagnosisSubTypeId);
            
            CreateTable(
                "dbo.PersonRelationship",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RelationshipFromPersonId = c.Int(nullable: false),
                        RelationWithPersonId = c.Int(nullable: false),
                        RelationshipTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.RelationshipFromPersonId)
                .ForeignKey("dbo.RelationshipType", t => t.RelationshipTypeId)
                .ForeignKey("dbo.Person", t => t.RelationWithPersonId)
                .Index(t => t.RelationshipFromPersonId)
                .Index(t => t.RelationWithPersonId)
                .Index(t => t.RelationshipTypeId);
            
            CreateTable(
                "dbo.RelationshipType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RelationshipTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.PersonRelationship", "RelationWithPersonId", "dbo.Person");
            DropForeignKey("dbo.PersonRelationship", "RelationshipTypeId", "dbo.RelationshipType");
            DropForeignKey("dbo.PersonRelationship", "RelationshipFromPersonId", "dbo.Person");
            DropForeignKey("dbo.Person", "EthnicityId", "dbo.Ethnicity");
            DropForeignKey("dbo.Person", "FamilyId", "dbo.Family");
            DropForeignKey("dbo.Family", "EthnicityID", "dbo.Ethnicity");
            DropForeignKey("dbo.Family", "DiagnosisSubTypeId", "dbo.DiagnosisSubType");
            DropForeignKey("dbo.Family", "DiagnosisID", "dbo.Diagnosis");
            DropForeignKey("dbo.Person", "DiagnosisSubTypeId", "dbo.DiagnosisSubType");
            DropForeignKey("dbo.DiagnosisSubType", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Person", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.PersonRelationship", new[] { "RelationshipTypeId" });
            DropIndex("dbo.PersonRelationship", new[] { "RelationWithPersonId" });
            DropIndex("dbo.PersonRelationship", new[] { "RelationshipFromPersonId" });
            DropIndex("dbo.Family", new[] { "DiagnosisSubTypeId" });
            DropIndex("dbo.Family", new[] { "DiagnosisID" });
            DropIndex("dbo.Family", new[] { "EthnicityID" });
            DropIndex("dbo.DiagnosisSubType", new[] { "DiagnosisId" });
            DropIndex("dbo.Person", new[] { "EthnicityId" });
            DropIndex("dbo.Person", new[] { "DiagnosisSubTypeId" });
            DropIndex("dbo.Person", new[] { "DiagnosisId" });
            DropIndex("dbo.Person", new[] { "FamilyId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.Error");
            DropTable("dbo.RelationshipType");
            DropTable("dbo.PersonRelationship");
            DropTable("dbo.Family");
            DropTable("dbo.Ethnicity");
            DropTable("dbo.DiagnosisSubType");
            DropTable("dbo.Person");
            DropTable("dbo.Diagnosis");
        }
    }
}
