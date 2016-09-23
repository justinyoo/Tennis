using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TournamentHistory.Mappers;
using TournamentHistory.Models;

namespace TournamentHistory.Services
{
    /// <summary>
    /// This represents the service entity for players.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private readonly ISyndicationFeedWrapper _syndication;
        private readonly IMapper _mapper;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerService"/> class.
        /// </summary>
        /// <param name="syndication"><see cref="IMapper"/> instance.</param>
        /// <param name="mapper"><see cref="IMapper"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="syndication"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null" />.</exception>
        public PlayerService(ISyndicationFeedWrapper syndication, IMapper mapper)
        {
            if (syndication == null)
            {
                throw new ArgumentNullException(nameof(syndication));
            }

            this._syndication = syndication;

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this._mapper = mapper;
        }

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        /// <returns>Returns the list of players.</returns>
        public async Task<List<PlayerModel>> GetPlayersAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the player details.</returns>
        public async Task<PlayerModel> GetPlayerAsync(long memberId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the list of <see cref="TournamentFeedModel"/> instances from RSS feed.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the list of <see cref="TournamentFeedModel"/> instances.</returns>
        public async Task<List<TournamentFeedModel>> GetTournamentsFromFeedAsync(long memberId)
        {
            var feed = await this._syndication.LoadAsync(memberId).ConfigureAwait(false);
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