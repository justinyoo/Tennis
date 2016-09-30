using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="CompetitionService"/> class.
    /// </summary>
    public interface ICompetitionService : IDisposable
    {
        /// <summary>
        /// Gets the list of competitions.
        /// </summary>
        /// <returns>Returns the list of competitions.</returns>
        Task<List<CompetitionModel>> GetCompetitionsAsync();
    }
}