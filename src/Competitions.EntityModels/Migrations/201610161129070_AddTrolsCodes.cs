namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrolsCodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Team", "TrolsTeamCode", c => c.String(maxLength: 32));
            AddColumn("dbo.Competition", "TrolsDaytimeCode", c => c.String(maxLength: 32));
            AddColumn("dbo.Competition", "TrolsSectionCode", c => c.String(maxLength: 32));
            AddColumn("dbo.District", "TrolsFixture", c => c.String(maxLength: 512));
            AddColumn("dbo.District", "TrolsResults", c => c.String(maxLength: 512));
            AddColumn("dbo.District", "TrolsLadders", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("dbo.District", "TrolsLadders");
            DropColumn("dbo.District", "TrolsResults");
            DropColumn("dbo.District", "TrolsFixture");
            DropColumn("dbo.Competition", "TrolsSectionCode");
            DropColumn("dbo.Competition", "TrolsDaytimeCode");
            DropColumn("dbo.Team", "TrolsTeamCode");
        }
    }
}
