namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ethnicity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EthnicityName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Family", "PrimaryRace");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Family", "PrimaryRace", c => c.Int(nullable: false));
            DropTable("dbo.Ethnicity");
        }
    }
}
