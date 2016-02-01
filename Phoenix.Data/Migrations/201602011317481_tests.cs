namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestTypeID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TestType", t => t.TestTypeID)
                .Index(t => t.TestTypeID);
            
            CreateTable(
                "dbo.TestAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        FieldType = c.String(),
                        FieldSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FieldLabel = c.String(),
                        Mandatory = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        TestAttributeValue_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Test", t => t.TestID)
                .ForeignKey("dbo.TestAttributeValue", t => t.TestAttributeValue_ID)
                .Index(t => t.TestID)
                .Index(t => t.TestAttributeValue_ID);
            
            CreateTable(
                "dbo.TestType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PatientTestAnswer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ValueString = c.String(),
                        ValueInt = c.Int(nullable: false),
                        ValueDec = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PersonID = c.Int(nullable: false),
                        TestAttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PersonID)
                .ForeignKey("dbo.TestAttribute", t => t.TestAttributeID)
                .Index(t => t.PersonID)
                .Index(t => t.TestAttributeID);
            
            CreateTable(
                "dbo.TestAttributeValue",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Text = c.String(),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TestPerson",
                c => new
                    {
                        Test_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_ID, t.Person_ID })
                .ForeignKey("dbo.Test", t => t.Test_ID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.Person_ID, cascadeDelete: true)
                .Index(t => t.Test_ID)
                .Index(t => t.Person_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestAttribute", "TestAttributeValue_ID", "dbo.TestAttributeValue");
            DropForeignKey("dbo.PatientTestAnswer", "TestAttributeID", "dbo.TestAttribute");
            DropForeignKey("dbo.PatientTestAnswer", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Test", "TestTypeID", "dbo.TestType");
            DropForeignKey("dbo.TestAttribute", "TestID", "dbo.Test");
            DropForeignKey("dbo.TestPerson", "Person_ID", "dbo.Person");
            DropForeignKey("dbo.TestPerson", "Test_ID", "dbo.Test");
            DropIndex("dbo.TestPerson", new[] { "Person_ID" });
            DropIndex("dbo.TestPerson", new[] { "Test_ID" });
            DropIndex("dbo.PatientTestAnswer", new[] { "TestAttributeID" });
            DropIndex("dbo.PatientTestAnswer", new[] { "PersonID" });
            DropIndex("dbo.TestAttribute", new[] { "TestAttributeValue_ID" });
            DropIndex("dbo.TestAttribute", new[] { "TestID" });
            DropIndex("dbo.Test", new[] { "TestTypeID" });
            DropTable("dbo.TestPerson");
            DropTable("dbo.TestAttributeValue");
            DropTable("dbo.PatientTestAnswer");
            DropTable("dbo.TestType");
            DropTable("dbo.TestAttribute");
            DropTable("dbo.Test");
        }
    }
}
