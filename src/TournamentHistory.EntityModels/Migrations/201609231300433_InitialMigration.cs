namespace TournamentHistory.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        MemberId = c.Long(),
                        ProfileId = c.Guid(),
                        RankingId = c.Long(),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        MiddleNames = c.String(maxLength: 256),
                        LastName = c.String(nullable: false, maxLength: 64),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.PlayerTournament",
                c => new
                    {
                        PlayerTournamentId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        TournamentId = c.Guid(nullable: false),
                        PlayerNumber = c.Int(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.PlayerTournamentId)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Tournament", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Tournament",
                c => new
                    {
                        TournamentId = c.Guid(nullable: false),
                        TournamentKey = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Summary = c.String(nullable: false, maxLength: 512),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerTournament", "TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.PlayerTournament", "PlayerId", "dbo.Player");
            DropIndex("dbo.PlayerTournament", new[] { "TournamentId" });
            DropIndex("dbo.PlayerTournament", new[] { "PlayerId" });
            DropTable("dbo.Tournament");
            DropTable("dbo.PlayerTournament");
            DropTable("dbo.Player");
        }
    }
}
