namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonRelationship",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        RelationWithPersonId = c.Int(nullable: false),
                        RelationshipType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dbo.Person", t => t.RelationWithPersonId)
                .Index(t => t.PersonId)
                .Index(t => t.RelationWithPersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonRelationship", "RelationWithPersonId", "dbo.Person");
            DropForeignKey("dbo.PersonRelationship", "PersonId", "dbo.Person");
            DropIndex("dbo.PersonRelationship", new[] { "RelationWithPersonId" });
            DropIndex("dbo.PersonRelationship", new[] { "PersonId" });
            DropTable("dbo.PersonRelationship");
        }
    }
}
