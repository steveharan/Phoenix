namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "FirstRegisteredDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Person", "DateFirstRegistered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "DateFirstRegistered", c => c.DateTime(nullable: false));
            DropColumn("dbo.Person", "FirstRegisteredDate");
        }
    }
}
