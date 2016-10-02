namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        ClubId = c.Guid(nullable: false),
                        VenueId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Manager = c.String(maxLength: 128),
                        Phone = c.String(maxLength: 16),
                        Mobile = c.String(maxLength: 16),
                        Email = c.String(maxLength: 256),
                        Url = c.String(maxLength: 512),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.ClubId)
                .ForeignKey("dbo.Venue", t => t.VenueId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.CompetitionClub",
                c => new
                    {
                        CompetitionClubId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        ClubId = c.Guid(nullable: false),
                        ClubTag = c.String(maxLength: 8),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.CompetitionClubId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Competition", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.CompetitionId)
                .Index(t => t.ClubId);
            
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
                "dbo.Fixture",
                c => new
                    {
                        FixtureId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        VenueId = c.Guid(nullable: false),
                        Week = c.Int(nullable: false),
                        DateScheduled = c.DateTimeOffset(nullable: false, precision: 7),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.Competition", t => t.CompetitionId, cascadeDelete: true)
                .ForeignKey("dbo.Venue", t => t.VenueId)
                .Index(t => t.CompetitionId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchId = c.Guid(nullable: false),
                        FixtureId = c.Guid(nullable: false),
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
                "dbo.MatchPlayer",
                c => new
                    {
                        MatchPlayerId = c.Guid(nullable: false),
                        MatchId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        HomeOrAway = c.String(nullable: false),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.MatchPlayerId)
                .ForeignKey("dbo.Match", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        ClubId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 64),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
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
                .PrimaryKey(t => t.VenueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Club", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.CompetitionClub", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.Fixture", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Player", "ClubId", "dbo.Club");
            DropForeignKey("dbo.MatchPlayer", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "FixtureId", "dbo.Fixture");
            DropForeignKey("dbo.Fixture", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.Competition", "DistrictId", "dbo.District");
            DropForeignKey("dbo.CompetitionClub", "ClubId", "dbo.Club");
            DropIndex("dbo.Player", new[] { "ClubId" });
            DropIndex("dbo.MatchPlayer", new[] { "PlayerId" });
            DropIndex("dbo.MatchPlayer", new[] { "MatchId" });
            DropIndex("dbo.Match", new[] { "FixtureId" });
            DropIndex("dbo.Fixture", new[] { "VenueId" });
            DropIndex("dbo.Fixture", new[] { "CompetitionId" });
            DropIndex("dbo.Competition", new[] { "DistrictId" });
            DropIndex("dbo.CompetitionClub", new[] { "ClubId" });
            DropIndex("dbo.CompetitionClub", new[] { "CompetitionId" });
            DropIndex("dbo.Club", new[] { "VenueId" });
            DropTable("dbo.Venue");
            DropTable("dbo.Player");
            DropTable("dbo.MatchPlayer");
            DropTable("dbo.Match");
            DropTable("dbo.Fixture");
            DropTable("dbo.District");
            DropTable("dbo.Competition");
            DropTable("dbo.CompetitionClub");
            DropTable("dbo.Club");
        }
    }
}
