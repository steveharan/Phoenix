namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientTest", "TestDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientTest", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientTest", "Notes");
            DropColumn("dbo.PatientTest", "TestDateTime");
        }
    }
}
