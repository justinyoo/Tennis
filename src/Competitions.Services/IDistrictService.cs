using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="DistrictService"/> class.
    /// </summary>
    public interface IDistrictService : IDisposable
    {
        /// <summary>
        /// Gets the list of districts.
        /// </summary>
        /// <returns>Returns the list of districts.</returns>
        Task<List<DistrictModel>> GetDistrictsAsync();
    }
}