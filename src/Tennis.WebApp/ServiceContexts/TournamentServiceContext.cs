using System;

using Tennis.Mappers;
using Tournaments.Services;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This represents the context entity for tournament related services.
    /// </summary>
    public class TournamentServiceContext : BaseServiceContext, ITournamentServiceContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentServiceContext"/> class.
        /// </summary>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <param name="feedService"><see cref="IFeedService"/> instance.</param>
        /// <param name="tournamentService"><see cref="ITournamentService"/> instance.</param>
        /// <param name="playerService"><see cref="IPlayerService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="feedService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="tournamentService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="playerService"/> is <see langword="null" />.</exception>
        public TournamentServiceContext(IMapperFactory mapperFactory,
                                        IFeedService feedService,
                                        ITournamentService tournamentService,
                                        IPlayerService playerService)
            : base(mapperFactory)
        {
            if (feedService == null)
            {
                throw new ArgumentNullException(nameof(feedService));
            }

            this.FeedService = feedService;

            if (tournamentService == null)
            {
                throw new ArgumentNullException(nameof(tournamentService));
            }

            this.TournamentService = tournamentService;

            if (playerService == null)
            {
                throw new ArgumentNullException(nameof(playerService));
            }

            this.PlayerService = playerService;
        }

        /// <summary>
        /// Gets the <see cref="IFeedService"/> instance.
        /// </summary>
        public IFeedService FeedService { get; }

        /// <summary>
        /// Gets the <see cref="ITournamentService"/> instance.
        /// </summary>
        public ITournamentService TournamentService { get; }

        /// <summary>
        /// Gets the <see cref="IPlayerService"/> instance.
        /// </summary>
        public IPlayerService PlayerService { get; }
    }
}
