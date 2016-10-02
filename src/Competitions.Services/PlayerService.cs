using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Competitions.EntityModels;
using Competitions.Mappers;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Services
{
    /// <summary>
    /// This represents the service entity for clubs.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public PlayerService(ICompetitionDbContext dbContext, IMapperFactory mapperFactory)
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
        /// Gets the list of players.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of clubs.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clubId"/> is <see langword="null" />.</exception>
        public async Task<List<PlayerModel>> GetPlayersAsync(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(clubId));
            }

            var results = await this._dbContext.Players
                                    .Where(p => p.ClubId == clubId)
                                    .Include(p => p.Club)
                                    .Include(p => p.Club.Venue)
                                    .OrderBy(p => p.LastName)
                                    .ThenBy(p => p.FirstName)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<PlayerToPlayerModelMapper>())
            {
                var players = mapper.Map<List<PlayerModel>>(results);

                return players;
            }
        }

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the player details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="playerId"/> is <see langword="null" />.</exception>
        public async Task<PlayerModel> GetPlayerAsync(Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            var result = await this._dbContext.Players
                                   .Include(p => p.Club)
                                   .Include(p => p.Club.Venue)
                                   .SingleOrDefaultAsync(p => p.PlayerId == playerId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<PlayerToPlayerModelMapper>())
            {
                var player = mapper.Map<PlayerModel>(result);

                return player;
            }
        }

        /// <summary>
        /// Saves the player details.
        /// </summary>
        /// <param name="model"><see cref="PlayerModel"/> instance.</param>
        /// <returns>Returns the player Id from the player details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SavePlayerAsync(PlayerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var player = await this.GetOrCreatePlayerAsync(model).ConfigureAwait(false);

            this._dbContext.Players.AddOrUpdate(player);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return player.PlayerId;
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

        private async Task<Player> GetOrCreatePlayerAsync(PlayerModel model)
        {
            var player = await this._dbContext.Players
                                   .SingleOrDefaultAsync(p => p.PlayerId == model.PlayerId)
                                   .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (player == null)
            {
                player = new Player() { PlayerId = Guid.NewGuid(), DateCreated = now };
            }

            player.ClubId = model.ClubId;
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.DateUpdated = now;

            return player;
        }

        private async Task<Venue> GetOrCreateVenueAsync(VenueModel model)
        {
            var venue = await this._dbContext.Venues
                                  .SingleOrDefaultAsync(p => p.VenueId == model.VenueId)
                                  .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (venue == null)
            {
                venue = new Venue() { VenueId = Guid.NewGuid(), DateCreated = now };
            }

            venue.Address1 = model.Address1;
            venue.Address2 = model.Address2;
            venue.Suburb = model.Suburb;
            venue.State = model.State;
            venue.Postcode = model.Postcode;
            venue.DateUpdated = now;

            return venue;
        }
    }
}
