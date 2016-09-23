using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TournamentHistory.Models;

namespace TournamentHistory.Services
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
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the player details.</returns>
        Task<PlayerModel> GetPlayerAsync(long memberId);

        /// <summary>
        /// Gets the list of <see cref="TournamentFeedModel"/> instances from RSS feed.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the list of <see cref="TournamentFeedModel"/> instances.</returns>
        Task<List<TournamentFeedModel>> GetTournamentsFromFeedAsync(long memberId);
    }
}