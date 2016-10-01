using System;
using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="BaseServiceContext"/> class.
    /// </summary>
    public interface IBaseServiceContext : IDisposable
    {
        /// <summary>
        /// Maps the object to designated typed one.
        /// </summary>
        /// <typeparam name="TMapper">Type of mapper.</typeparam>
        /// <typeparam name="TDestination">Type to be mapped.</typeparam>
        /// <param name="item">Item to map.</param>
        /// <returns>Returns the mapped object.</returns>
        TDestination Map<TMapper, TDestination>(object item) where TMapper : IMapper;
    }
}