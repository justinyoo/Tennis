namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSetNumberToMatch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Match", "SetNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Match", "SetNumber");
        }
    }
}
