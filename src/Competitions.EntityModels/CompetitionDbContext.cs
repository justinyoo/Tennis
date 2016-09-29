using System.Data.Entity;

using Competitions.EntityModels.Mapping;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the db context entity for tournament.
    /// </summary>
    public class CompetitionDbContext : DbContext, ICompetitionDbContext
    {
        /// <summary>
        /// Initialises static members of the <see cref="CompetitionDbContext"/> class.
        /// </summary>
        static CompetitionDbContext()
        {
            Database.SetInitializer<CompetitionDbContext>(null);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionDbContext"/> class.
        /// </summary>
        public CompetitionDbContext()
            : base("Name=CompetitionDbContext")
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">DB connection string.</param>
        public CompetitionDbContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Gets or sets the set of <see cref="Club"/> instances.
        /// </summary>
        public DbSet<Club> Clubs { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Competition"/> instances.
        /// </summary>
        public DbSet<Competition> Competitions { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="District"/> instances.
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Fixture"/> instances.
        /// </summary>
        public DbSet<Fixture> Fixtures { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Match"/> instances.
        /// </summary>
        public DbSet<Match> Matches { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="MatchPlayer"/> instances.
        /// </summary>
        public DbSet<MatchPlayer> MatchPlayers { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Player"/> instances.
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Venue"/> instances.
        /// </summary>
        public DbSet<Venue> Venues { get; set; }

        /// <summary>
        /// Called when the model is being created.
        /// </summary>
        /// <param name="builder"><see cref="DbModelBuilder"/> instance that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new ClubMap());
            builder.Configurations.Add(new CompetitionMap());
            builder.Configurations.Add(new DistrictMap());
            builder.Configurations.Add(new FixtureMap());
            builder.Configurations.Add(new MatchMap());
            builder.Configurations.Add(new MatchPlayerMap());
            builder.Configurations.Add(new PlayerMap());
            builder.Configurations.Add(new VenueMap());
        }
    }
}
