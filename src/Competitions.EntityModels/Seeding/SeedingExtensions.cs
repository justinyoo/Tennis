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
                                new District() { DistrictId = Guid.NewGuid(), Name = "Bayside Regional Tennis Association", Url = "http://www.baysidetennis.asn.au/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Eastern Region Tennis Inc.", Url = "http://www.ertinc.org.au/", DateCreated = now, DateUpdated = now },
                                new District() { DistrictId = Guid.NewGuid(), Name = "Waverley & District Tennis Association", Url = "http://www.waverleytennis.asn.au/", DateCreated = now, DateUpdated = now },
                            };
            context.Districts.AddOrUpdate(districts);

            return context;
        }
    }
}
