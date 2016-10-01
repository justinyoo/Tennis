using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Competitions.EntityModels;
using Competitions.Mappers;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Services
{
    /// <summary>
    /// This represents the service entity for venues.
    /// </summary>
    public class VenueService : IVenueService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="VenueService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public VenueService(ICompetitionDbContext dbContext, IMapperFactory mapperFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;

            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }

            this._mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the list of districts.
        /// </summary>
        /// <returns>Returns the list of districts.</returns>
        public async Task<List<VenueModel>> GetVenuesAsync()
        {
            var results = await this._dbContext.Venues
                                    .OrderBy(p => p.State)
                                    .ThenBy(p => p.Suburb)
                                    .ThenBy(p => p.Address1)
                                    .ThenBy(p => p.Address2)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<VenueToVenueModelMapper>())
            {
                var venues = mapper.Map<List<VenueModel>>(results);

                return venues;
            }
        }

        /// <summary>
        /// Gets the venue details.
        /// </summary>
        /// <param name="venueId">Venue Id.</param>
        /// <returns>Returns the venue details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="venueId"/> is <see langword="null" />.</exception>
        public async Task<VenueModel> GetVenueAsync(Guid venueId)
        {
            if (venueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(venueId));
            }

            var result = await this._dbContext.Venues
                                   .SingleOrDefaultAsync(p => p.VenueId == venueId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<VenueToVenueModelMapper>())
            {
                var venue = mapper.Map<VenueModel>(result);

                return venue;
            }
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
