using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

using Tournaments.Helpers;

namespace Tournaments.Services
{
    /// <summary>
    /// This represents the context entity for RSS feed.
    /// </summary>
    public class FeedContext : IFeedContext
    {
        private readonly IFeedHelper _helper;
        private readonly ISyndicationFeedWrapper _syndication;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="FeedContext"/> class.
        /// </summary>
        /// <param name="helper"><see cref="IFeedHelper"/> instance.</param>
        /// <param name="syndication"><see cref="ISyndicationFeedWrapper"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="syndication"/> is <see langword="null" />.</exception>
        public FeedContext(IFeedHelper helper, ISyndicationFeedWrapper syndication)
        {
            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            this._helper = helper;

            if (syndication == null)
            {
                throw new ArgumentNullException(nameof(syndication));
            }

            this._syndication = syndication;
        }

        /// <summary>
        /// Gets the first name, middle names and last name from the feed title.
        /// </summary>
        /// <param name="title">Feed title.</param>
        /// <returns>Returns the first name, middle names and last name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> is <see langword="null" />.</exception>
        public async Task<List<string>> GetNamesFromFeedAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            return await this._helper.GetNamesFromTitleAsync(title).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the member Id from the given feed URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the member Id from the given feed URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<long> GetMemberIdFromFeedAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return await this._helper.GetMemberIdFromUrlAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the tournament key from the given link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the tournament from the given link URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<Guid> GetTournamentKeyFromFeedAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return await this._helper.GetTournamentKeyFromUrlAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the player number from the given link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the player number from the given link URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<int> GetPlayerNumberFromFeedAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return await this._helper.GetPlayerNumberFromUrlAsync(url).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Loads the feed for the member Id of tennis.com.au
        ///// </summary>
        ///// <param name="url">Feed URL at tennis.com.au.</param>
        ///// <returns>Returns the <see cref="System.ServiceModel.Syndication.SyndicationFeed"/> instance.</returns>
        ///// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        //public async Task<SyndicationFeed> LoadFeedAsync(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url))
        //    {
        //        throw new ArgumentNullException(nameof(url));
        //    }

        //    return await this._syndication.LoadAsync(url).ConfigureAwait(false);
        //}

        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="System.ServiceModel.Syndication.SyndicationFeed"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">MemberId is less than or equal to zero.</exception>
        public async Task<SyndicationFeed> LoadFeedAsync(long memberId)
        {
            if (memberId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return await this._syndication.LoadAsync(memberId).ConfigureAwait(false);
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
