namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeam : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Team", "CompetitionId", "dbo.Competition");
            DropIndex("dbo.Team", new[] { "CompetitionId" });
            AddColumn("dbo.Team", "Tag", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Team", "CompetitionId", c => c.Guid());
            CreateIndex("dbo.Team", "CompetitionId");
            AddForeignKey("dbo.Team", "CompetitionId", "dbo.Competition", "CompetitionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "CompetitionId", "dbo.Competition");
            DropIndex("dbo.Team", new[] { "CompetitionId" });
            AlterColumn("dbo.Team", "CompetitionId", c => c.Guid(nullable: false));
            DropColumn("dbo.Team", "Tag");
            CreateIndex("dbo.Team", "CompetitionId");
            AddForeignKey("dbo.Team", "CompetitionId", "dbo.Competition", "CompetitionId", cascadeDelete: true);
        }
    }
}
