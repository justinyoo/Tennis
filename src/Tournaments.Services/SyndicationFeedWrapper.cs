using System;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Tournaments.Services
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
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the <see cref="SyndicationFeed"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<SyndicationFeed> LoadAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) })
            using (var stream = await client.GetStreamAsync(url).ConfigureAwait(false))
            using (var reader = XmlReader.Create(stream))
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
        /// <exception cref="ArgumentOutOfRangeException">MemberId is less than or equal to zero.</exception>
        public async Task<SyndicationFeed> LoadAsync(long memberId)
        {
            if (memberId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var url = string.Format(FeedUrl, memberId);
            var feed = await this.LoadAsync(url).ConfigureAwait(false);

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
