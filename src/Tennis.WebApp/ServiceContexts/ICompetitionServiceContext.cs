using Competitions.Services;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="CompetitionServiceContext"/> class.
    /// </summary>
    public interface ICompetitionServiceContext : IBaseServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IDistrictService"/> instance.
        /// </summary>
        IDistrictService DistrictService { get; }

        /// <summary>
        /// Gets the <see cref="ICompetitionService"/> instance.
        /// </summary>
        ICompetitionService CompetitionService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        IClubService ClubService { get; }
    }
}