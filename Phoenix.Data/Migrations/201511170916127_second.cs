namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Family", new[] { "DiagnosisId" });
            CreateIndex("dbo.Family", "DiagnosisID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Family", new[] { "DiagnosisID" });
            CreateIndex("dbo.Family", "DiagnosisId");
        }
    }
}
