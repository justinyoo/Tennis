using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="ClubService"/> class.
    /// </summary>
    public interface IClubService : IDisposable
    {
        /// <summary>
        /// Gets the list of clubs.
        /// </summary>
        /// <returns>Returns the list of clubs.</returns>
        Task<List<ClubModel>> GetClubsAsync();

        /// <summary>
        /// Gets the club details.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the club details.</returns>
        Task<ClubModel> GetClubAsync(Guid clubId);

        /// <summary>
        /// Saves the club details.
        /// </summary>
        /// <param name="model"><see cref="ClubModel"/> instance.</param>
        /// <returns>Returns the club Id from the club details.</returns>
        Task<Guid> SaveClubAsync(ClubModel model);
    }
}