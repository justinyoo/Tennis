using Tournaments.Services;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="TournamentServiceContext"/> class.
    /// </summary>
    public interface ITournamentServiceContext : IBaseServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IFeedService"/> instance.
        /// </summary>
        IFeedService FeedService { get; }

        /// <summary>
        /// Gets the <see cref="ITournamentService"/> instance.
        /// </summary>
        ITournamentService TournamentService { get; }

        /// <summary>
        /// Gets the <see cref="IPlayerService"/> instance.
        /// </summary>
        IPlayerService PlayerService { get; }
    }
}