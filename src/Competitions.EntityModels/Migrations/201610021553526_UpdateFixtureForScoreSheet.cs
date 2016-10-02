namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFixtureForScoreSheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fixture", "ScoreSheet", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fixture", "ScoreSheet");
        }
    }
}
