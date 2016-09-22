using System;
using System.Collections.Generic;

using TournamentHistory.Models;

namespace TournamentHistory.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="PlayerService"/> class.
    /// </summary>
    public interface IPlayerService : IDisposable
    {
        /// <summary>
        /// Gets the list of <see cref="TournamentFeedModel"/> instances from RSS feed.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the list of <see cref="TournamentFeedModel"/> instances.</returns>
        List<TournamentFeedModel> GetTournamentsFromFeed(long playerId);
    }
}