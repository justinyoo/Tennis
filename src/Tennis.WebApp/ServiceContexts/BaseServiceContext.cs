using System;

using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This represents the base service context entity. This MUST be inherited.
    /// </summary>
    public abstract class BaseServiceContext : IBaseServiceContext
    {
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseServiceContext"/> class.
        /// </summary>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        protected BaseServiceContext(IMapperFactory mapperFactory)
        {
            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }

            this._mapperFactory = mapperFactory;

        }

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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value that specifies whether to be disposing resources or not.</param>
        protected void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed resources.
            }

            // Dispose unmanaged resources.

            this._disposed = true;
        }
    }
}