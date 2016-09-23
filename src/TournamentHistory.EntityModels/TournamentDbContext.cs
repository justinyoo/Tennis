using System.Data.Entity;

using TournamentHistory.EntityModels.Mapping;

namespace TournamentHistory.EntityModels
{
    /// <summary>
    /// This represents the db context entity for tournament.
    /// </summary>
    public class TournamentDbContext : DbContext, ITournamentDbContext
    {
        /// <summary>
        /// Initialises static members of the <see cref="TournamentDbContext"/> class.
        /// </summary>
        static TournamentDbContext()
        {
            Database.SetInitializer<TournamentDbContext>(null);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentDbContext"/> class.
        /// </summary>
        public TournamentDbContext()
            : base("Name=TournamentDbContext")
        {
        }

        /// <summary>
        /// Gets or sets the set of <see cref="Player"/> instances.
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="PlayerTournament"/> instances.
        /// </summary>
        public DbSet<PlayerTournament> PlayerTournaments { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Tournament"/> instances.
        /// </summary>
        public DbSet<Tournament> Tournaments { get; set; }

        /// <summary>
        /// Called when the model is being created.
        /// </summary>
        /// <param name="builder"><see cref="DbModelBuilder"/> instance that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new PlayerMap());
            builder.Configurations.Add(new PlayerTournamentMap());
            builder.Configurations.Add(new TournamentMap());
        }
    }
}
