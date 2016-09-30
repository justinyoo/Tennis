using System;
using System.Collections.Generic;

using Competitions.Services;

using Tennis.Mappers;

namespace Tennis.WebApp.ServiceContexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="CompetitionServiceContext"/> class.
    /// </summary>
    public interface ICompetitionServiceContext : IDisposable
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
        /// Maps the object to designated typed one.
        /// </summary>
        /// <typeparam name="TMapper">Type of mapper.</typeparam>
        /// <typeparam name="TDestination">Type to be mapped.</typeparam>
        /// <param name="item">Item to map.</param>
        /// <returns>Returns the mapped object.</returns>
        TDestination Map<TMapper, TDestination>(object item) where TMapper : IMapper;
    }
}