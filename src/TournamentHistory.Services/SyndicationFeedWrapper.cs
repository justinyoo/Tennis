using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace TournamentHistory.Services
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="SyndicationFeed"/> class.
    /// </summary>
    public class SyndicationFeedWrapper : ISyndicationFeedWrapper
    {
        private const string FeedUrl = "http://tournaments.tennis.com.au/feed/player.aspx?memberid={0}";

        private bool _disposed;

        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        public SyndicationFeed Load(long memberId)
        {
            var url = string.Format(FeedUrl, memberId);

            using (var reader = XmlReader.Create(url))
            {
                var feed = SyndicationFeed.Load(reader);

                return feed;
            }
        }

        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        public async Task<SyndicationFeed> LoadAsync(long memberId)
        {
            var feed = await Task.Factory.StartNew(() => this.Load(memberId)).ConfigureAwait(false);

            return feed;
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
    }
}
