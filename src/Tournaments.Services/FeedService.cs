using System;
using System.Threading.Tasks;

using Tennis.Mappers;

using Tournaments.Mappers;
using Tournaments.Models;

namespace Tournaments.Services
{
    /// <summary>
    /// This represents the service entity for RSS feed.
    /// </summary>
    public class FeedService : IFeedService
    {
        private readonly IFeedContext _feedContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="FeedService"/> class.
        /// </summary>
        /// <param name="feedContext"><see cref="IFeedContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="feedContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public FeedService(IFeedContext feedContext, IMapperFactory mapperFactory)
        {
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
        /// Gets the member Id from the RSS feed URL.
        /// </summary>
        /// <param name="feedUrl">RSS feed URL.</param>
        /// <returns>Returns the member Id.</returns>
        public async Task<long> GetMemberIdFromFeedUrlAsync(string feedUrl)
        {
            var memberId = await this._feedContext.GetMemberIdFromFeedAsync(feedUrl).ConfigureAwait(false);

            return memberId;
        }

        /// <summary>
        /// Gets the tournament feed details corresponding to the member Id.
        /// </summary>
        /// <param name="memberId">Member Id from tennis.com.au.</param>
        /// <returns>Returns the <see cref="TournamentFeedModel"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid member Id..</exception>
        public async Task<TournamentFeedModel> GetTournamentFeedByMemberIdAsync(long memberId)
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
