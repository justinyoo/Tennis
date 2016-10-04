namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        private const string DistrictSeedTemplate = "INSERT INTO District([DistrictId], [Name], [Url], [TrolsUrl], [DateCreated], [DateUpdated]) VALUES (NEWID(), '{0}', '{1}', '{2}', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET())";

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
                        ScoreSheet = c.String(maxLength: 256),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Competition", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.ClubId)
                .Index(t => t.CompetitionId);
            
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
                "dbo.Team",
                c => new
                    {
                        TeamId = c.Guid(nullable: false),
                        ClubId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Competition", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.ClubId)
                .Index(t => t.CompetitionId);
            
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

            Sql(string.Format(DistrictSeedTemplate, "Bayside Regional Tennis Association", "http://www.baysidetennis.asn.au/", "http://trols.org.au/brta/"));
            Sql(string.Format(DistrictSeedTemplate, "Berwick & District Tennis Association", "http://www.bdta.com.au/", "http://trols.org.au/bdta/"));
            Sql(string.Format(DistrictSeedTemplate, "Blackburn & District Night Tennis", "http://www.bdnta.com/", "http://trols.org.au/bdnta/"));
            Sql(string.Format(DistrictSeedTemplate, "Diamond Valley Tennis Association", string.Empty, "http://trols.org.au/dvta/"));
            Sql(string.Format(DistrictSeedTemplate, "Eastern Region Tennis Incorporated", "http://www.ertinc.org.au/", "http://www.ertinc.org.au/erta/"));
            Sql(string.Format(DistrictSeedTemplate, "Metro Masters Tennis Association", string.Empty, "http://trols.org.au/metro/"));
            Sql(string.Format(DistrictSeedTemplate, "Moorabbin & District Junior Tennis", "http://www.mdjta.com.au/", "http://trols.org.au/mdjta/"));
            Sql(string.Format(DistrictSeedTemplate, "North Eastern Junior Tennis", "http://www.tennis.com.au/nejta", "http://trols.org.au/nejta/"));
            Sql(string.Format(DistrictSeedTemplate, "North Eastern Night Tennis Group", string.Empty, "http://trols.org.au/nentg/"));
            Sql(string.Format(DistrictSeedTemplate, "Waverley Tennis", "http://www.waverleytennis.asn.au/", "http://trols.org.au/wdta/"));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Venue", "VenueId", "dbo.Club");
            DropForeignKey("dbo.Fixture", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.TeamPlayer", "TeamId", "dbo.Team");
            DropForeignKey("dbo.TeamPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.MatchPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.MatchPlayer", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "FixtureId", "dbo.Fixture");
            DropForeignKey("dbo.Team", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.Team", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Competition", "DistrictId", "dbo.District");
            DropForeignKey("dbo.Fixture", "ClubId", "dbo.Club");
            DropIndex("dbo.Venue", new[] { "VenueId" });
            DropIndex("dbo.Match", new[] { "FixtureId" });
            DropIndex("dbo.MatchPlayer", new[] { "PlayerId" });
            DropIndex("dbo.MatchPlayer", new[] { "MatchId" });
            DropIndex("dbo.TeamPlayer", new[] { "TeamId" });
            DropIndex("dbo.TeamPlayer", new[] { "PlayerId" });
            DropIndex("dbo.Team", new[] { "CompetitionId" });
            DropIndex("dbo.Team", new[] { "ClubId" });
            DropIndex("dbo.Competition", new[] { "DistrictId" });
            DropIndex("dbo.Fixture", new[] { "CompetitionId" });
            DropIndex("dbo.Fixture", new[] { "ClubId" });
            DropTable("dbo.Venue");
            DropTable("dbo.Match");
            DropTable("dbo.MatchPlayer");
            DropTable("dbo.Player");
            DropTable("dbo.TeamPlayer");
            DropTable("dbo.Team");
            DropTable("dbo.District");
            DropTable("dbo.Competition");
            DropTable("dbo.Fixture");
            DropTable("dbo.Club");
        }
    }
}
