namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMatchPlayerPlayerId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player");
            DropIndex("dbo.MatchPlayer", new[] { "PlayerId" });
            AlterColumn("dbo.MatchPlayer", "PlayerId", c => c.Guid());
            CreateIndex("dbo.MatchPlayer", "PlayerId");
            AddForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player", "PlayerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player");
            DropIndex("dbo.MatchPlayer", new[] { "PlayerId" });
            AlterColumn("dbo.MatchPlayer", "PlayerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.MatchPlayer", "PlayerId");
            AddForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player", "PlayerId", cascadeDelete: true);
        }
    }
}
