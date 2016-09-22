using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

using TournamentHistory.Mappers;
using TournamentHistory.Models;

namespace TournamentHistory.Services
{
    /// <summary>
    /// This represents the service entity for players.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private const string FeedUrl = "http://tournaments.tennis.com.au/feed/player.aspx?memberid={0}";

        private readonly IMapper _mapper;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerService"/> class.
        /// </summary>
        /// <param name="mapper"><see cref="IMapper"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null" />.</exception>
        public PlayerService(IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this._mapper = mapper;
        }

        /// <summary>
        /// Gets the list of <see cref="TournamentFeedModel"/> instances from RSS feed.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the list of <see cref="TournamentFeedModel"/> instances.</returns>
        public List<TournamentFeedModel> GetTournamentsFromFeed(long playerId)
        {
            SyndicationFeed feed;
            using (var reader = XmlReader.Create(string.Format(FeedUrl, playerId)))
            {
                feed = SyndicationFeed.Load(reader);
            }

            var items = feed.Items.ToList();
            var tournaments = this._mapper.Map<List<TournamentFeedModel>>(items);

            return tournaments;
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