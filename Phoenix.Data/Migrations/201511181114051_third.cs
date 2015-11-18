namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "DateFirstRegistered", c => c.DateTime(nullable: false));
            AddColumn("dbo.Person", "Notes", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "Notes");
            DropColumn("dbo.Person", "DateFirstRegistered");
        }
    }
}
