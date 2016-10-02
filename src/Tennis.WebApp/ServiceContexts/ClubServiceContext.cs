using System;

using Competitions.Services;

using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This represents the context entity for club related services.
    /// </summary>
    public class ClubServiceContext : BaseServiceContext, IClubServiceContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ClubServiceContext"/> class.
        /// </summary>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <param name="playerService"><see cref="IPlayerService"/> insntance.</param>
        /// <param name="clubService"><see cref="IClubService"/> insntance.</param>
        /// <param name="venueService"><see cref="IVenueService"/> insntance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="venueService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="clubService"/> is <see langword="null" />.</exception>
        public ClubServiceContext(IMapperFactory mapperFactory,
                                  IPlayerService playerService,
                                  IClubService clubService,
                                  IVenueService venueService)
            : base(mapperFactory)
        {
            if (playerService == null)
            {
                throw new ArgumentNullException(nameof(playerService));
            }

            this.PlayerService = playerService;

            if (clubService == null)
            {
                throw new ArgumentNullException(nameof(clubService));
            }

            this.ClubService = clubService;

            if (venueService == null)
            {
                throw new ArgumentNullException(nameof(venueService));
            }

            this.VenueService = venueService;
        }

        /// <summary>
        /// Gets the <see cref="IPlayerService"/> instance.
        /// </summary>
        public IPlayerService PlayerService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        public IClubService ClubService { get; }

        /// <summary>
        /// Gets the <see cref="IVenueService"/> instance.
        /// </summary>
        public IVenueService VenueService { get; }
    }
}