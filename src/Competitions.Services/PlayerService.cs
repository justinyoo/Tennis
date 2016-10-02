﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

using Competitions.EntityModels;
using Competitions.Mappers;
using Competitions.Models;

using Tennis.Common.Extensions;
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
        /// Saves match details.
        /// </summary>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <param name="homePlayers">List of home players.</param>
        /// <param name="awayPlayers">List of away players.</param>
        /// <returns>Returns the list of matches.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fixtureId"/> is <see langword="null" />.</exception>
        public async Task<List<Guid>> SaveMatchesAsync(Guid fixtureId, List<Guid> homePlayers, List<Guid> awayPlayers)
        {
            if (fixtureId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(fixtureId));
            }

            var matches = await this.GetOrCreateMatchesAsync(fixtureId, homePlayers, awayPlayers).ConfigureAwait(false);

            this._dbContext.Matches.AddOrUpdate(matches.ToArray());

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return matches.Select(p => p.MatchId).ToList();
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

        private async Task<List<Match>> GetOrCreateMatchesAsync(Guid fixtureId, List<Guid> homePlayers, List<Guid> awayPlayers)
        {
            var matches = await this._dbContext.Matches
                                    .Include(p => p.MatchPlayers)
                                    .Where(p => p.FixtureId == fixtureId)
                                    .OrderBy(p => p.SetNumber)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (matches.IsNullOrEmpty())
            {
                matches = Enumerable.Range(1, 6)
                                    .Select(p => new Match() { MatchId = Guid.NewGuid(), FixtureId = fixtureId, SetNumber = p, DateCreated = now })
                                    .ToList();
            }

            // For singles.
            foreach (var i in Enumerable.Range(1, 6).Where(p => p%2 == 1))
            {
                var match = matches[i - 1];
                if (match.MatchPlayers.IsNullOrEmpty())
                {
                    match.MatchPlayers = new List<MatchPlayer>()
                                         {
                                             new MatchPlayer() { MatchPlayerId = Guid.NewGuid(), MatchId = match.MatchId, DateCreated = now },
                                             new MatchPlayer() { MatchPlayerId = Guid.NewGuid(), MatchId = match.MatchId, DateCreated = now },
                                         };
                }

                if (!homePlayers.IsNullOrEmpty())
                {
                    match.MatchPlayers[0].PlayerId = homePlayers[i - 1];
                }

                match.MatchPlayers[0].HomeOrAway = "Home";
                match.MatchPlayers[0].DateUpdated = now;

                if (!awayPlayers.IsNullOrEmpty())
                {
                    match.MatchPlayers[1].PlayerId = awayPlayers[i - 1];
                }

                match.MatchPlayers[1].HomeOrAway = "Away";
                match.MatchPlayers[1].DateUpdated = now;

                match.DateUpdated = now;
            }

            // For doubles.
            foreach (var i in Enumerable.Range(1, 6).Where(p => p % 2 == 0))
            {
                var match = matches[i - 1];
                if (match.MatchPlayers.IsNullOrEmpty())
                {
                    match.MatchPlayers = new List<MatchPlayer>()
                                         {
                                             new MatchPlayer() { MatchPlayerId = Guid.NewGuid(), MatchId = match.MatchId, DateCreated = now },
                                             new MatchPlayer() { MatchPlayerId = Guid.NewGuid(), MatchId = match.MatchId, DateCreated = now },
                                             new MatchPlayer() { MatchPlayerId = Guid.NewGuid(), MatchId = match.MatchId, DateCreated = now },
                                             new MatchPlayer() { MatchPlayerId = Guid.NewGuid(), MatchId = match.MatchId, DateCreated = now },
                                         };
                }

                var first = i % 3;
                var next = (i + 1) % 3;

                if (!homePlayers.IsNullOrEmpty())
                {
                    match.MatchPlayers[0].PlayerId = homePlayers[first];
                    match.MatchPlayers[1].PlayerId = homePlayers[next];
                }

                match.MatchPlayers[0].HomeOrAway = "Home";
                match.MatchPlayers[0].DateUpdated = now;

                match.MatchPlayers[1].HomeOrAway = "Home";
                match.MatchPlayers[1].DateUpdated = now;

                if (!awayPlayers.IsNullOrEmpty())
                {
                    match.MatchPlayers[2].PlayerId = awayPlayers[first];
                    match.MatchPlayers[3].PlayerId = awayPlayers[next];
                }

                match.MatchPlayers[2].HomeOrAway = "Away";
                match.MatchPlayers[2].DateUpdated = now;

                match.MatchPlayers[3].HomeOrAway = "Away";
                match.MatchPlayers[3].DateUpdated = now;

                match.DateUpdated = now;
            }

            return matches;
        }
    }
}
