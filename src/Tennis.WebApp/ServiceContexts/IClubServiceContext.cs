using System;
using System.Collections.Generic;

using Competitions.Services;

using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="ClubServiceContext"/> class.
    /// </summary>
    public interface IClubServiceContext : IBaseServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IVelueService"/> instance.
        /// </summary>
        IVenueService VenueService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        IClubService ClubService { get; }
    }

    /// <summary>
    /// This represents the context entity for club related services.
    /// </summary>
    public class ClubServiceContext : BaseServiceContext, IClubServiceContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ClubServiceContext"/> class.
        /// </summary>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="venueService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="clubService"/> is <see langword="null" />.</exception>
        public ClubServiceContext(IMapperFactory mapperFactory, IVenueService venueService, IClubService clubService)
            : base(mapperFactory)
        {
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
        }

        /// <summary>
        /// Gets the <see cref="IVelueService"/> instance.
        /// </summary>
        public IVenueService VenueService { get; }

        /// <summary>
        /// Gets the <see cref="IClubService"/> instance.
        /// </summary>
        public IClubService ClubService { get; }
    }
}