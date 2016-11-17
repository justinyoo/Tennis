﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Tournaments.Models;

namespace Tournaments.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="PlayerService"/> class.
    /// </summary>
    public interface IPlayerService : IDisposable
    {
        /// <summary>
        /// Gets the list of players.
        /// </summary>
        /// <returns>Returns the list of players.</returns>
        Task<List<PlayerModel>> GetPlayersAsync();

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the player details.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Member Id is less than or equal to zero.</exception>
        Task<PlayerModel> GetPlayerAsync(Guid playerId);

        /// <summary>
        /// Gets the list of <see cref="TournamentFeedModel"/> instances from RSS feed.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the list of <see cref="TournamentFeedModel"/> instances.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Member Id is less than or equal to zero.</exception>
        Task<TournamentFeedModel> GetTournamentsFromFeedAsync(long memberId);

        /// <summary>
        /// Saves the tournaments details from RSS feed to database.
        /// </summary>
        /// <param name="feedUrl">Feed URL at tennis.com.au.</param>
        /// <exception cref="ArgumentNullException"><paramref name="feedUrl"/> is <see langword="null" />.</exception>
        Task<PlayerModel> SaveTournamentsFromFeedAsync(string feedUrl);

        /// <summary>
        /// Saves player details.
        /// </summary>
        /// <param name="model"><see cref="PlayerModel"/> instance.</param>
        /// <returns>Returns the player Id.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        Task<Guid> SavePlayerAsync(PlayerModel model);

        /// <summary>
        /// Saves the player-tournament relations to database.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <param name="tournamentId">Tournament Id.</param>
        /// <param name="playerNumber">Player number from tennis.com.au</param>
        /// <returns>Returns player-tournament Id.</returns>
        Task<Guid> SavePlayerTournamentAsync(Guid playerId, Guid tournamentId, int playerNumber);
    }
}