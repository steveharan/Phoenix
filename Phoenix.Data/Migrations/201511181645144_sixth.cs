namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "DateDeceased", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Person", "Deleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Deleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Person", "DateDeceased");
        }
    }
}
