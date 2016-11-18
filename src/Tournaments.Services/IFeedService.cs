using System;
using System.Threading.Tasks;

using Tournaments.Models;

namespace Tournaments.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="FeedService"/> class.
    /// </summary>
    public interface IFeedService : IDisposable
    {
        /// <summary>
        /// Gets the member Id from the RSS feed URL.
        /// </summary>
        /// <param name="feedUrl">RSS feed URL.</param>
        /// <returns>Returns the member Id.</returns>
        Task<long> GetMemberIdFromFeedUrlAsync(string feedUrl);

        /// <summary>
        /// Gets the tournament feed details corresponding to the member Id.
        /// </summary>
        /// <param name="memberId">Member Id from tennis.com.au.</param>
        /// <returns>Returns the <see cref="TournamentFeedModel"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid member Id..</exception>
        Task<TournamentFeedModel> GetTournamentFeedByMemberIdAsync(long memberId);
    }
}