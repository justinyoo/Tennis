using System;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace TournamentHistory.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="SyndicationFeedWrapper"/> class.
    /// </summary>
    public interface ISyndicationFeedWrapper : IDisposable
    {
        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        SyndicationFeed Load(long memberId);

        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        Task<SyndicationFeed> LoadAsync(long memberId);
    }
}