using System;

using Competitions.Services;

using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This represents the context entity for competition related services.
    /// </summary>
    public class CompetitionServiceContext : ICompetitionServiceContext
    {
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionServiceContext"/> class.
        /// </summary>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <param name="districtService"><see cref="IDistrictService"/> instance.</param>
        /// <param name="competitionService"><see cref="ICompetitionService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="districtService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="competitionService"/> is <see langword="null" />.</exception>
        public CompetitionServiceContext(IMapperFactory mapperFactory, IDistrictService districtService, ICompetitionService competitionService)
        {
            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }

            this._mapperFactory = mapperFactory;

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
        /// Maps the object to designated typed one.
        /// </summary>
        /// <typeparam name="TMapper">Type of mapper.</typeparam>
        /// <typeparam name="TDestination">Type to be mapped.</typeparam>
        /// <param name="item">Item to map.</param>
        /// <returns>Returns the mapped object.</returns>
        public TDestination Map<TMapper, TDestination>(object item) where TMapper : IMapper
        {
            using (var mapper = this._mapperFactory.Get<TMapper>())
            {
                var result = mapper.Map<TDestination>(item);
                return result;
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
