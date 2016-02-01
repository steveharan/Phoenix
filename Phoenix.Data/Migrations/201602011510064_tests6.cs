namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PatientTest", "PatientID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientTest", "PatientID", c => c.Int(nullable: false));
        }
    }
}
