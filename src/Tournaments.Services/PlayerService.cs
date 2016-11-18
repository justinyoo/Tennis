using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

using Tennis.Mappers;

using Tournaments.EntityModels;
using Tournaments.Mappers;
using Tournaments.Models;

namespace Tournaments.Services
{
    /// <summary>
    /// This represents the service entity for players.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private readonly ITournamentDbContext _dbContext;
        private readonly IFeedContext _feedContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ITournamentDbContext"/> instance.</param>
        /// <param name="feedContext"><see cref="IFeedContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="feedContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public PlayerService(ITournamentDbContext dbContext, IFeedContext feedContext, IMapperFactory mapperFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;

            if (feedContext == null)
            {
                throw new ArgumentNullException(nameof(feedContext));
            }

            this._feedContext = feedContext;

            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }

            this._mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        /// <returns>Returns the list of players.</returns>
        public async Task<List<PlayerModel>> GetPlayersAsync()
        {
            var results = await this._dbContext.Players
                                    .OrderBy(p => p.FirstName)
                                    .ThenBy(p => p.MiddleNames)
                                    .ThenBy(p => p.LastName)
                                    .ThenBy(p => p.MemberId)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<PlayerToPlayerModelMapper>())
            {
                var players = mapper.Map<List<PlayerModel>>(results);

                return players;
            }
        }

        /// <summary>
        /// Gets the list of player's memberId from tennis.com.au.
        /// </summary>
        /// <returns>Returns the list of player's memberId from tennis.com.au.</returns>
        public async Task<List<long>> GetPlayerMemberIdsAsync()
        {
            var players = await this.GetPlayersAsync().ConfigureAwait(false);
            var memberIds = players.Where(p => p.MemberId.HasValue)
                                   .Select(p => p.MemberId.Value)
                                   .ToList();

            return memberIds;
        }

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the player details.</returns>
        /// <exception cref="ArgumentException">Invalid PlayerId</exception>
        public async Task<PlayerModel> GetPlayerAsync(Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentException("Invalid PlayerId");
            }

            var results = await this._dbContext.PlayerTournaments
                                   .Include(p => p.Player)
                                   .Include(p => p.Tournament)
                                   .Where(p => p.PlayerId == playerId)
                                   .ToListAsync()
                                   .ConfigureAwait(false);

            using (var mapper1 = this._mapperFactory.Get<PlayerToPlayerModelMapper>())
            using (var mapper2 = this._mapperFactory.Get<PlayerTournamentToTournamentModelMapper>())
            {
                var player = mapper1.Map<PlayerModel>(results.First().Player);
                player.Tournaments = mapper2.Map<List<TournamentModel>>(results);

                return player;
            }
        }

        /// <summary>
        /// Gets the names from the RSS feed title.
        /// </summary>
        /// <param name="feedTitle">Feed title.</param>
        /// <returns>Returns the names.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="feedTitle"/> is <see langword="null" />.</exception>
        public async Task<List<string>> GetPlayerNameFromFeedAsync(string feedTitle)
        {
            if (string.IsNullOrWhiteSpace(feedTitle))
            {
                throw new ArgumentNullException(nameof(feedTitle));
            }

            var names = await this._feedContext.GetNamesFromFeedAsync(feedTitle).ConfigureAwait(false);

            return names;
        }

        /// <summary>
        /// Gets the list of <see cref="TournamentFeedModel"/> instances from RSS feed.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the list of <see cref="TournamentFeedModel"/> instances.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Member Id is less than or equal to zero.</exception>
        public async Task<TournamentFeedModel> GetTournamentsFromFeedAsync(long memberId)
        {
            if (memberId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(memberId));
            }

            var feed = await this._feedContext.LoadFeedAsync(memberId).ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<FeedToTournamentFeedModelMapper>())
            {
                var tournament = mapper.Map<TournamentFeedModel>(feed);

                return tournament;
            }
        }

        ///// <summary>
        ///// Saves the tournaments details from RSS feed to database.
        ///// </summary>
        ///// <param name="feedUrl">Feed URL at tennis.com.au.</param>
        ///// <returns>Returns the number of state entries written to the underlying database</returns>
        ///// <exception cref="ArgumentNullException"><paramref name="feedUrl"/> is <see langword="null" />.</exception>
        //public async Task<PlayerModel> SaveTournamentsFromFeedAsync(string feedUrl)
        //{
        //    if (string.IsNullOrWhiteSpace(feedUrl))
        //    {
        //        throw new ArgumentNullException(nameof(feedUrl));
        //    }

        //    var memberId = await this._feedContext.GetMemberIdFromFeedAsync(feedUrl).ConfigureAwait(false);
        //    var feed = await this.GetTournamentsFromFeedAsync(memberId).ConfigureAwait(false);
        //    var names = await this._feedContext.GetNamesFromFeedAsync(feed.Title).ConfigureAwait(false);

        //    var player = await this.GetOrCreatePlayerAsync(memberId, names).ConfigureAwait(false);

        //    foreach (var item in feed.Items)
        //    {
        //        var tournamentKey = await this._feedContext.GetTournamentKeyFromFeedAsync(item.ItemId).ConfigureAwait(false);
        //        var playerNumber = await this._feedContext.GetPlayerNumberFromFeedAsync(item.ItemId).ConfigureAwait(false);

        //        var tournament = await this.GetOrCreateTournamentAsync(tournamentKey, item).ConfigureAwait(false);
        //        await this.GetOrCreatePlayerTournamentAsync(player.PlayerId, tournament.TournamentId, playerNumber).ConfigureAwait(false);
        //    }

        //    var transaction = this._dbContext.Database.BeginTransaction();
        //    try
        //    {
        //        await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        //        transaction.Commit();
                
        //        return await this.GetPlayerAsync(player.PlayerId).ConfigureAwait(false);
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //        throw;
        //    }
        //}

        /// <summary>
        /// Saves player details.
        /// </summary>
        /// <param name="memberId">Member Id.</param>
        /// <param name="names">Collection of names - first name, middle names, and last name.</param>
        /// <returns>Returns the player Id.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid member Id..</exception>
        /// <exception cref="ArgumentNullException"><paramref name="names"/> is <see langword="null" />.</exception>
        public async Task<Guid> SavePlayerAsync(long memberId, List<string> names)
        {
            if (memberId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(memberId));
            }

            if (names == null || !names.Any())
            {
                throw new ArgumentNullException(nameof(names));
            }

            var player = await this.GetOrCreatePlayerAsync(memberId, names).ConfigureAwait(false);

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
        /// Saves player details.
        /// </summary>
        /// <param name="model"><see cref="PlayerModel"/> instance.</param>
        /// <returns>Returns the player Id.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SavePlayerAsync(PlayerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var player = await this.GetOrCreatePlayerAsync(model).ConfigureAwait(false);

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
        /// Saves the player-tournament relations to database.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <param name="tournamentId">Tournament Id.</param>
        /// <param name="playerNumber">Player number from tennis.com.au</param>
        /// <returns>Returns player-tournament Id.</returns>
        /// <exception cref="ArgumentException">Invalid PlayerId.</exception>
        /// <exception cref="ArgumentException">Invalid TournamentId.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Player number must be greater than zero.</exception>
        public async Task<Guid> SavePlayerTournamentAsync(Guid playerId, Guid tournamentId, int playerNumber)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentException("Invalid PlayerId.");
            }

            if (tournamentId == Guid.Empty)
            {
                throw new ArgumentException("Invalid TournamentId.");
            }

            if (playerNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(playerNumber));
            }

            var pt = await this.GetOrCreatePlayerTournamentAsync(playerId, tournamentId, playerNumber).ConfigureAwait(false);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return pt.PlayerTournamentId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Saves the player-tournament relations to database.
        /// </summary>
        /// <param name="tournamentId">Tournament Id.</param>
        /// <param name="model"><see cref="PlayerTournamentFeedItemModel"/> instance.</param>
        /// <returns>Returns player-tournament Id.</returns>
        /// <exception cref="ArgumentException">Invalid TournamentId.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SavePlayerTournamentAsync(Guid tournamentId, PlayerTournamentFeedItemModel model)
        {
            if (tournamentId == Guid.Empty)
            {
                throw new ArgumentException("Invalid TournamentId.");
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var playerNumber = await this._feedContext.GetPlayerNumberFromFeedAsync(model.Item.ItemId).ConfigureAwait(false);

            return await this.SavePlayerTournamentAsync(model.PlayerId, tournamentId, playerNumber).ConfigureAwait(false);
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
                                   .SingleOrDefaultAsync(p => p.MemberId == model.MemberId)
                                   .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (player == null)
            {
                player = new Player() { PlayerId = Guid.NewGuid(), DateCreated = now };
            }

            player.MemberId = model.MemberId;
            player.ProfileId = model.ProfileId;
            player.RankingId = model.RankingId;
            player.FirstName = model.FirstName;
            player.MiddleNames = model.MiddleNames;
            player.LastName = model.LastName;
            player.DateUpdated = now;

            this._dbContext.Players.AddOrUpdate(player);

            return player;
        }

        /// <summary>
        /// Gets or creates a <see cref="Player"/> instance corresponding to the member Id.
        /// </summary>
        /// <param name="memberId">Member Id from tennis.com.au.</param>
        /// <param name="names">Player name.</param>
        /// <returns>Returns the <see cref="Player"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Member Id is less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="names"/> is <see langword="null" />.</exception>
        private async Task<Player> GetOrCreatePlayerAsync(long memberId, List<string> names)
        {
            var player = await this._dbContext.Players
                                   .SingleOrDefaultAsync(p => p.MemberId == memberId)
                                   .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (player == null)
            {
                player = new Player() { PlayerId = Guid.NewGuid(), DateCreated = now };
            }

            player.MemberId = memberId;
            player.FirstName = names.First();
            player.MiddleNames = names.Skip(1).First();
            player.LastName = names.Last();
            player.DateUpdated = now;

            this._dbContext.Players.AddOrUpdate(player);

            return player;
        }

        /// <summary>
        /// Gets or creates a <see cref="Tournament"/> instance corresponding to the tournament key.
        /// </summary>
        /// <param name="tournamentKey">Tournament key from tennis.com.au.</param>
        /// <param name="item"><see cref="TournamentFeedItemModel"/> instance.</param>
        /// <returns>Returns the <see cref="Tournament"/> instance.</returns>
        /// <exception cref="ArgumentException">Invalid TournamentKey</exception>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null" />.</exception>
        private async Task<Tournament> GetOrCreateTournamentAsync(Guid tournamentKey, TournamentFeedItemModel item)
        {
            if (tournamentKey == Guid.Empty)
            {
                throw new ArgumentException("Invalid TournamentKey");
            }

            var now = DateTimeOffset.Now;

            var tournament = await this._dbContext.Tournaments
                                       .SingleOrDefaultAsync(p => p.TournamentKey == tournamentKey)
                                       .ConfigureAwait(false) ??
                             new Tournament()
                             {
                                 TournamentId = Guid.NewGuid(),
                                 TournamentKey = tournamentKey,
                                 DateCreated = now,
                             };


            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (tournament.DatePublished == item.DatePublished)
            {
                return tournament;
            }

            tournament.Title = item.Title;
            tournament.Summary = item.Summary;
            tournament.DatePublished = item.DatePublished;
            tournament.DateUpdated = now;

            this._dbContext.Tournaments.AddOrUpdate(tournament);

            return tournament;
        }

        /// <summary>
        /// Gets or creates a <see cref="PlayerTournament"/> instance corresponding to the player Id and tournament Id.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <param name="tournamentId">Tournament Id.</param>
        /// <param name="playerNumber">Player number assigned to the tournament.</param>
        /// <returns>Returns the <see cref="PlayerTournament"/> instance.</returns>
        /// <exception cref="ArgumentException">Invalid playerId</exception>
        /// <exception cref="ArgumentException">Invalid TournamentId</exception>
        private async Task<PlayerTournament> GetOrCreatePlayerTournamentAsync(Guid playerId, Guid tournamentId, int playerNumber)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentException("Invalid PlayerId");
            }

            if (tournamentId == Guid.Empty)
            {
                throw new ArgumentException("Invalid TournamentId");
            }

            var pt = await this._dbContext.PlayerTournaments
                               .SingleOrDefaultAsync(p => p.PlayerId == playerId && p.TournamentId == tournamentId)
                               .ConfigureAwait(false);

            if (pt != null)
            {
                return pt;
            }

            var now = DateTimeOffset.Now;

            pt = new PlayerTournament()
                 {
                     PlayerTournamentId = Guid.NewGuid(),
                     PlayerId = playerId,
                     TournamentId = tournamentId,
                     PlayerNumber = playerNumber,
                     DateCreated = now,
                     DateUpdated = now,
                 };

            this._dbContext.PlayerTournaments.AddOrUpdate(pt);

            return pt;
        }
    }
}