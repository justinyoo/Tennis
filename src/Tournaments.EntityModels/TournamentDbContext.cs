using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;

using Tournaments.EntityModels.Mapping;

namespace Tournaments.EntityModels
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
        /// Initialises a new instance of the <see cref="TournamentDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">DB connection string.</param>
        public TournamentDbContext(string connectionString)
            : base(connectionString)
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
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        public override async Task<int> SaveChangesAsync()
        {
            if (this.Database.Connection.State != ConnectionState.Open)
            {
                await this.Database.Connection.OpenAsync().ConfigureAwait(false);
            }

            var transaction = this.Database.BeginTransaction();
            try
            {
                var result = await base.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                if (this.Database.Connection.State != ConnectionState.Closed)
                {
                    this.Database.Connection.Close();
                }
            }
        }

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
