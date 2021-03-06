namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClubPlayer",
                c => new
                    {
                        ClubPlayerId = c.Guid(nullable: false),
                        ClubId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.ClubPlayerId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.ClubId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        ClubId = c.Guid(nullable: false),
                        VenueId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Manager = c.String(maxLength: 128),
                        ClubHousePhone = c.String(maxLength: 16),
                        Phone = c.String(maxLength: 16),
                        Mobile = c.String(maxLength: 16),
                        Email = c.String(maxLength: 256),
                        Url = c.String(maxLength: 512),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.ClubId);
            
            CreateTable(
                "dbo.Fixture",
                c => new
                    {
                        FixtureId = c.Guid(nullable: false),
                        ClubId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        Week = c.Int(nullable: false),
                        DateScheduled = c.DateTimeOffset(nullable: false, precision: 7),
                        HomeTeamId = c.Guid(nullable: false),
                        AwayTeamId = c.Guid(nullable: false),
                        ScoreSheet = c.String(maxLength: 256),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.Team", t => t.AwayTeamId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Competition", t => t.CompetitionId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.HomeTeamId)
                .Index(t => t.ClubId)
                .Index(t => t.CompetitionId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamId = c.Guid(nullable: false),
                        ClubId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Tag = c.String(maxLength: 16),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
            CreateTable(
                "dbo.CompetitionTeam",
                c => new
                    {
                        CompetitionTeamId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        TeamId = c.Guid(nullable: false),
                        TeamNumber = c.Int(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.CompetitionTeamId)
                .ForeignKey("dbo.Competition", t => t.CompetitionId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.CompetitionId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Competition",
                c => new
                    {
                        CompetitionId = c.Guid(nullable: false),
                        DistrictId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Year = c.Int(nullable: false),
                        Season = c.String(nullable: false, maxLength: 8),
                        Day = c.String(nullable: false, maxLength: 16),
                        Type = c.String(nullable: false, maxLength: 8),
                        Grade = c.String(nullable: false, maxLength: 32),
                        Level = c.String(nullable: false, maxLength: 8),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.CompetitionId)
                .ForeignKey("dbo.District", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        DistrictId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Url = c.String(maxLength: 512),
                        TrolsUrl = c.String(maxLength: 512),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.DistrictId);
            
            CreateTable(
                "dbo.TeamPlayer",
                c => new
                    {
                        TeamPlayerId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        TeamId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.TeamPlayerId)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 64),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.MatchPlayer",
                c => new
                    {
                        MatchPlayerId = c.Guid(nullable: false),
                        MatchId = c.Guid(nullable: false),
                        PlayerId = c.Guid(),
                        HomeOrAway = c.String(nullable: false, maxLength: 8),
                        PlayerNumber = c.Int(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.MatchPlayerId)
                .ForeignKey("dbo.Match", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.MatchId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchId = c.Guid(nullable: false),
                        FixtureId = c.Guid(nullable: false),
                        SetNumber = c.Int(nullable: false),
                        HomeSetScore = c.Int(nullable: false),
                        AwaySetScore = c.Int(nullable: false),
                        HomeGameScore = c.Int(nullable: false),
                        AwayGameScore = c.Int(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Fixture", t => t.FixtureId, cascadeDelete: true)
                .Index(t => t.FixtureId);
            
            CreateTable(
                "dbo.Venue",
                c => new
                    {
                        VenueId = c.Guid(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 256),
                        Address2 = c.String(maxLength: 256),
                        Suburb = c.String(nullable: false, maxLength: 64),
                        State = c.String(nullable: false, maxLength: 32),
                        Postcode = c.String(nullable: false, maxLength: 4),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.VenueId)
                .ForeignKey("dbo.Club", t => t.VenueId)
                .Index(t => t.VenueId);
            
            this.SeedDistricts();
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClubPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.ClubPlayer", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Venue", "VenueId", "dbo.Club");
            DropForeignKey("dbo.Fixture", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.Fixture", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.Fixture", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Fixture", "AwayTeamId", "dbo.Team");
            DropForeignKey("dbo.TeamPlayer", "TeamId", "dbo.Team");
            DropForeignKey("dbo.TeamPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.MatchPlayer", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "FixtureId", "dbo.Fixture");
            DropForeignKey("dbo.CompetitionTeam", "TeamId", "dbo.Team");
            DropForeignKey("dbo.CompetitionTeam", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.Competition", "DistrictId", "dbo.District");
            DropForeignKey("dbo.Team", "ClubId", "dbo.Club");
            DropIndex("dbo.Venue", new[] { "VenueId" });
            DropIndex("dbo.Match", new[] { "FixtureId" });
            DropIndex("dbo.MatchPlayer", new[] { "PlayerId" });
            DropIndex("dbo.MatchPlayer", new[] { "MatchId" });
            DropIndex("dbo.TeamPlayer", new[] { "TeamId" });
            DropIndex("dbo.TeamPlayer", new[] { "PlayerId" });
            DropIndex("dbo.Competition", new[] { "DistrictId" });
            DropIndex("dbo.CompetitionTeam", new[] { "TeamId" });
            DropIndex("dbo.CompetitionTeam", new[] { "CompetitionId" });
            DropIndex("dbo.Team", new[] { "ClubId" });
            DropIndex("dbo.Fixture", new[] { "AwayTeamId" });
            DropIndex("dbo.Fixture", new[] { "HomeTeamId" });
            DropIndex("dbo.Fixture", new[] { "CompetitionId" });
            DropIndex("dbo.Fixture", new[] { "ClubId" });
            DropIndex("dbo.ClubPlayer", new[] { "PlayerId" });
            DropIndex("dbo.ClubPlayer", new[] { "ClubId" });
            DropTable("dbo.Venue");
            DropTable("dbo.Match");
            DropTable("dbo.MatchPlayer");
            DropTable("dbo.Player");
            DropTable("dbo.TeamPlayer");
            DropTable("dbo.District");
            DropTable("dbo.Competition");
            DropTable("dbo.CompetitionTeam");
            DropTable("dbo.Team");
            DropTable("dbo.Fixture");
            DropTable("dbo.Club");
            DropTable("dbo.ClubPlayer");
        }
    }
}
