using Competitions.EntityModels.Seeding;

namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Competitions.EntityModels.CompetitionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Competitions.EntityModels.CompetitionDbContext context)
        {
            //var now = DateTimeOffset.Now;

            //context.SeedDistricts(now);
        }
    }
}
