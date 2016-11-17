using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

using Tennis.Mappers;

using Tournaments.EntityModels;
using Tournaments.Mappers;
using Tournaments.Models;

namespace Tournaments.Services
{
    /// <summary>
    /// This represents the service entity for tournament.
    /// </summary>
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ITournamentDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public TournamentService(ITournamentDbContext dbContext, IMapperFactory mapperFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;

            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }

            this._mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the tournament details corresponding to the tournament Id.
        /// </summary>
        /// <param name="tournamentId">Tournament Id.</param>
        /// <returns>Returns the <see cref="TournamentModel"/> instance.</returns>
        /// <exception cref="ArgumentException">Invalid TournamentId.</exception>
        public async Task<TournamentModel> GetTournamentByIdAsync(Guid tournamentId)
        {
            if (tournamentId == Guid.Empty)
            {
                throw new ArgumentException("Invalid TournamentId.");
            }

            var result = await this._dbContext.Tournaments
                                   .SingleOrDefaultAsync(p => p.TournamentId == tournamentId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<TournamentToTournamentModelMapper>())
            {
                var tournament = mapper.Map<TournamentModel>(result);

                return tournament;
            }
        }

        /// <summary>
        /// Gets the tournament details corresponding to the tournament key.
        /// </summary>
        /// <param name="tournamentKey">Tournament key from tennis.com.au.</param>
        /// <returns>Returns the <see cref="TournamentModel"/> instance.</returns>
        /// <exception cref="ArgumentException">Invalid TournamentKey.</exception>
        public async Task<TournamentModel> GetTournamentByKeyAsync(Guid tournamentKey)
        {
            if (tournamentKey == Guid.Empty)
            {
                throw new ArgumentException("Invalid TournamentKey.");
            }

            var result = await this._dbContext.Tournaments
                                   .SingleOrDefaultAsync(p => p.TournamentKey == tournamentKey)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<TournamentToTournamentModelMapper>())
            {
                var tournament = mapper.Map<TournamentModel>(result);

                return tournament;
            }
        }

        /// <summary>
        /// Saves the tournament details.
        /// </summary>
        /// <param name="model"><see cref="TournamentModel"/> instance.</param>
        /// <returns>Returns the tournament Id.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SaveTournamentAsync(TournamentModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var tournament = await this.GetOrCreateTournamentAsync(model).ConfigureAwait(false);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return tournament.TournamentId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private async Task<Tournament> GetOrCreateTournamentAsync(TournamentModel model)
        {
            var tournament = await this._dbContext.Tournaments
                                       .SingleOrDefaultAsync(p => p.TournamentKey == model.TournamentKey)
                                       .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (tournament == null)
            {
                tournament = new Tournament() { TournamentId = Guid.NewGuid(), DateCreated = now };
            }

            if (tournament.DatePublished == model.DatePublished)
            {
                return tournament;
            }

            tournament.TournamentKey = model.TournamentKey;
            tournament.Title = model.Title;
            tournament.Summary = model.Summary;
            tournament.DatePublished = model.DatePublished;
            tournament.DateUpdated = now;

            this._dbContext.Tournaments.AddOrUpdate(tournament);

            return tournament;
        }
    }
}
