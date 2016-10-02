using Competitions.Services;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="ClubServiceContext"/> class.
    /// </summary>
    public interface IClubServiceContext : IBaseServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IPlayerService"/> instance.
        /// </summary>
        IPlayerService PlayerService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        IClubService ClubService { get; }

        /// <summary>
        /// Gets the <see cref="IVenueService"/> instance.
        /// </summary>
        IVenueService VenueService { get; }
    }
}