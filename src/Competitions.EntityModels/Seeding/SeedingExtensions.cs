using System;
using System.Data.Entity.Migrations;

namespace Competitions.EntityModels.Seeding
{
    /// <summary>
    /// This represents the seeding entity for district.
    /// </summary>
    public static class SeedingExtensions
    {
        /// <summary>
        /// Seeds districts.
        /// </summary>
        /// <param name="context"><see cref="CompetitionDbContext"/> instance.</param>
        /// <param name="now">Current <see cref="DateTimeOffset"/> value.</param>
        public static CompetitionDbContext SeedDistricts(this CompetitionDbContext context, DateTimeOffset now)
        {
            var districts = new[]
                            {
                                new District() { DistrictId = Guid.NewGuid(), Name = "Bayside Regional Tennis Association", Url = "http://www.baysidetennis.asn.au/", TrolsUrl = "http://trols.org.au/brta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Berwick & District Tennis Association", Url = "http://www.bdta.com.au/", TrolsUrl = "http://trols.org.au/bdta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Blackburn & District Night Tennis", Url = "http://www.bdnta.com/", TrolsUrl = "http://trols.org.au/bdnta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Diamond Valley Tennis Association", Url = null, TrolsUrl = "http://trols.org.au/dvta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Eastern Region Tennis Incorporated", Url = "http://www.ertinc.org.au/", TrolsUrl = "http://www.ertinc.org.au/erta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Metro Masters Tennis Association", Url = null, TrolsUrl = "http://trols.org.au/metro/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Moorabbin & District Junior Tennis", Url = "http://www.mdjta.com.au/", TrolsUrl = "http://trols.org.au/mdjta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "North Eastern Junior Tennis", Url = "http://www.tennis.com.au/nejta", TrolsUrl = "http://trols.org.au/nejta/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "North Eastern Night Tennis Group", Url = null, TrolsUrl = "http://trols.org.au/nentg/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Waverley Tennis", Url = "http://www.waverleytennis.asn.au/", TrolsUrl = "http://trols.org.au/wdta/", DateCreated = now, DateUpdated = now },
                            };
            context.Districts.AddOrUpdate(districts);

            return context;
        }
    }
}
