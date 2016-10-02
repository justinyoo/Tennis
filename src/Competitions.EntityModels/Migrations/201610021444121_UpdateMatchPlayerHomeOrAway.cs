namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMatchPlayerHomeOrAway : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MatchPlayer", "HomeOrAway", c => c.String(nullable: false, maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MatchPlayer", "HomeOrAway", c => c.String(nullable: false));
        }
    }
}
