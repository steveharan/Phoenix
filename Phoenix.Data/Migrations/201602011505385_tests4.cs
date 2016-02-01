namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestPerson", "Test_ID", "dbo.Test");
            DropForeignKey("dbo.TestPerson", "Person_ID", "dbo.Person");
            DropIndex("dbo.TestPerson", new[] { "Test_ID" });
            DropIndex("dbo.TestPerson", new[] { "Person_ID" });
            CreateTable(
                "dbo.PatientTest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        TestID = c.Int(nullable: false),
                        Person_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.Person_ID)
                .ForeignKey("dbo.Test", t => t.TestID)
                .Index(t => t.TestID)
                .Index(t => t.Person_ID);
            
            DropTable("dbo.TestPerson");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestPerson",
                c => new
                    {
                        Test_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_ID, t.Person_ID });
            
            DropForeignKey("dbo.PatientTest", "TestID", "dbo.Test");
            DropForeignKey("dbo.PatientTest", "Person_ID", "dbo.Person");
            DropIndex("dbo.PatientTest", new[] { "Person_ID" });
            DropIndex("dbo.PatientTest", new[] { "TestID" });
            DropTable("dbo.PatientTest");
            CreateIndex("dbo.TestPerson", "Person_ID");
            CreateIndex("dbo.TestPerson", "Test_ID");
            AddForeignKey("dbo.TestPerson", "Person_ID", "dbo.Person", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TestPerson", "Test_ID", "dbo.Test", "ID", cascadeDelete: true);
        }
    }
}
