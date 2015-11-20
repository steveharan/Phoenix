namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seventh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonRelationship", "Person_ID", c => c.Int());
            CreateIndex("dbo.PersonRelationship", "Person_ID");
            AddForeignKey("dbo.PersonRelationship", "Person_ID", "dbo.Person", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonRelationship", "Person_ID", "dbo.Person");
            DropIndex("dbo.PersonRelationship", new[] { "Person_ID" });
            DropColumn("dbo.PersonRelationship", "Person_ID");
        }
    }
}
