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
        /// <param name="venueService"><see cref="IVenueService"/> instance.</param>
        /// <param name="clubService"><see cref="IClubService"/> instance.</param>
        /// <param name="teamService"><see cref="ITeamService"/> instance.</param>
        /// <param name="competitionService"><see cref="ICompetitionService"/> instance.</param>
        /// <param name="fixtureService"><see cref="IFixtureService"/> instance.</param>
        /// <param name="playerService"><see cref="IPlayerService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="districtService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="competitionService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="clubService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="fixtureService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="playerService"/> is <see langword="null" />.</exception>
        public CompetitionServiceContext(IMapperFactory mapperFactory,
                                         IDistrictService districtService,
                                         IVenueService venueService,
                                         IClubService clubService,
                                         ITeamService teamService,
                                         ICompetitionService competitionService,
                                         IFixtureService fixtureService,
                                         IPlayerService playerService)
            : base(mapperFactory)
        {
            if (districtService == null)
            {
                throw new ArgumentNullException(nameof(districtService));
            }

            this.DistrictService = districtService;

            if (venueService == null)
            {
                throw new ArgumentNullException(nameof(venueService));
            }

            this.VenueService = venueService;

            if (clubService == null)
            {
                throw new ArgumentNullException(nameof(clubService));
            }

            this.ClubService = clubService;

            if (teamService == null)
            {
                throw new ArgumentNullException(nameof(teamService));
            }

            this.TeamService = teamService;

            if (competitionService == null)
            {
                throw new ArgumentNullException(nameof(competitionService));
            }

            this.CompetitionService = competitionService;

            if (fixtureService == null)
            {
                throw new ArgumentNullException(nameof(fixtureService));
            }

            this.FixtureService = fixtureService;

            if (playerService == null)
            {
                throw new ArgumentNullException(nameof(playerService));
            }

            this.PlayerService = playerService;
        }

        /// <summary>
        /// Gets the <see cref="IDistrictService"/> instance.
        /// </summary>
        public IDistrictService DistrictService { get; }

        /// <summary>
        /// Gets the <see cref="IVenueService"/> instance.
        /// </summary>
        public IVenueService VenueService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        public IClubService ClubService { get; }

        /// <summary>
        /// Gets the <see cref="ITeamService"/> instance.
        /// </summary>
        public ITeamService TeamService { get; set; }

        /// <summary>
        /// Gets the <see cref="ICompetitionService"/> instance.
        /// </summary>
        public ICompetitionService CompetitionService { get; }

        /// <summary>
        /// Gets the <see cref="IFixtureService"/> instance.
        /// </summary>
        public IFixtureService FixtureService { get; }

        /// <summary>
        /// Gets the <see cref="IPlayerService"/> instance.
        /// </summary>
        public IPlayerService PlayerService { get; }
    }
}
