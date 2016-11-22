using System;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Tournaments.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="SyndicationFeedWrapper"/> class.
    /// </summary>
    public interface ISyndicationFeedWrapper : IDisposable
    {
        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        Task<SyndicationFeed> LoadAsync(string url);

        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        Task<SyndicationFeed> LoadAsync(long memberId);
    }
}