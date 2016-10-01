using System.Data.Entity;
using System.Threading.Tasks;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This provides interfaces to the <see cref="CompetitionDbContext"/> class.
    /// </summary>
    public interface ICompetitionDbContext
    {
        /// <summary>
        /// Gets or sets the set of <see cref="Club"/> instances.
        /// </summary>
        DbSet<Club> Clubs { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="CompetitionClub"/> instances.
        /// </summary>
        DbSet<CompetitionClub> CompetitionClubs { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Competition"/> instances.
        /// </summary>
        DbSet<Competition> Competitions { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="District"/> instances.
        /// </summary>
        DbSet<District> Districts { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Fixture"/> instances.
        /// </summary>
        DbSet<Fixture> Fixtures { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Match"/> instances.
        /// </summary>
        DbSet<Match> Matches { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="MatchPlayer"/> instances.
        /// </summary>
        DbSet<MatchPlayer> MatchPlayers { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Player"/> instances.
        /// </summary>
        DbSet<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Venue"/> instances.
        /// </summary>
        DbSet<Venue> Venues { get; set; }

        /// <summary>
        /// Gets the <see cref="System.Data.Entity.Database"/> instance.
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        int SaveChanges();

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
        Task<int> SaveChangesAsync();
    }
}