namespace Phoenix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestAttribute", "TestAttributeValue_ID", "dbo.TestAttributeValue");
            DropPrimaryKey("dbo.TestAttributeValue");
            AddColumn("dbo.TestAttributeValue", "TestAttributeValueID", c => c.Int(nullable: false));
            AlterColumn("dbo.TestAttributeValue", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TestAttributeValue", "ID");
            AddForeignKey("dbo.TestAttribute", "TestAttributeValue_ID", "dbo.TestAttributeValue", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestAttribute", "TestAttributeValue_ID", "dbo.TestAttributeValue");
            DropPrimaryKey("dbo.TestAttributeValue");
            AlterColumn("dbo.TestAttributeValue", "ID", c => c.Int(nullable: false));
            DropColumn("dbo.TestAttributeValue", "TestAttributeValueID");
            AddPrimaryKey("dbo.TestAttributeValue", "ID");
            AddForeignKey("dbo.TestAttribute", "TestAttributeValue_ID", "dbo.TestAttributeValue", "ID");
        }
    }
}
