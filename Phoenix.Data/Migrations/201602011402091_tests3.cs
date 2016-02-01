namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientTestAnswer", "ValueInt", c => c.Int());
            AlterColumn("dbo.PatientTestAnswer", "ValueDec", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientTestAnswer", "ValueDec", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PatientTestAnswer", "ValueInt", c => c.Int(nullable: false));
        }
    }
}
