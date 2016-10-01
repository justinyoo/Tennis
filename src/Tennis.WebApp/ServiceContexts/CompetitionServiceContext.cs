using System;

using Competitions.Services;

using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This represents the context entity for competition related services.
    /// </summary>
    public class CompetitionServiceContext : BaseServiceContext, ICompetitionServiceContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionServiceContext"/> class.
        /// </summary>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <param name="districtService"><see cref="IDistrictService"/> instance.</param>
        /// <param name="competitionService"><see cref="ICompetitionService"/> instance.</param>
        /// <param name="clubService"><see cref="IClubService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="districtService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="competitionService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="clubService"/> is <see langword="null" />.</exception>
        public CompetitionServiceContext(IMapperFactory mapperFactory, IDistrictService districtService, ICompetitionService competitionService, IClubService clubService)
            : base(mapperFactory)
        {
            if (districtService == null)
            {
                throw new ArgumentNullException(nameof(districtService));
            }

            this.DistrictService = districtService;

            if (competitionService == null)
            {
                throw new ArgumentNullException(nameof(competitionService));
            }

            this.CompetitionService = competitionService;

            if (clubService == null)
            {
                throw new ArgumentNullException(nameof(clubService));
            }

            this.ClubService = clubService;
        }

        /// <summary>
        /// Gets the <see cref="IDistrictService"/> instance.
        /// </summary>
        public IDistrictService DistrictService { get; }

        /// <summary>
        /// Gets the <see cref="ICompetitionService"/> instance.
        /// </summary>
        public ICompetitionService CompetitionService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        public IClubService ClubService { get; }
    }
}
