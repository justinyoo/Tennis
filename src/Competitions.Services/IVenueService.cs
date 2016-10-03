using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="VenueService"/> class.
    /// </summary>
    public interface IVenueService : IDisposable
    {
        /// <summary>
        /// Gets the list of states.
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetStatesAsync();

        /// <summary>
        /// Gets the list of venues.
        /// </summary>
        /// <returns>Returns the list of venues.</returns>
        Task<List<VenueModel>> GetVenuesAsync();

        /// <summary>
        /// Gets the venue details.
        /// </summary>
        /// <param name="venueId">Venue Id.</param>
        /// <returns>Returns the venue details.</returns>
        Task<VenueModel> GetVenueAsync(Guid venueId);
    }
}