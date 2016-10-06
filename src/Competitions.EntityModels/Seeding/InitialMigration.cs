using System.Data.Entity.Migrations;

namespace Competitions.EntityModels.Migrations
{
    /// <summary>
    /// This represents database migration entity for initial seeding.
    /// </summary>
    public partial class InitialMigration : DbMigration
    {
        private const string DistrictSeedTemplate = "INSERT INTO District([DistrictId], [Name], [Url], [TrolsUrl], [DateCreated], [DateUpdated]) VALUES (NEWID(), '{0}', '{1}', '{2}', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET())";

        private void SeedDistricts()
        {
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
    }
}