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

        /// <summary>
        /// Gets the competition details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the competition details.</returns>
        Task<CompetitionModel> GetCompetitionAsync(Guid competitionId);

        /// <summary>
        /// Saves the competition details.
        /// </summary>
        /// <param name="model"><see cref="CompetitionModel"/> instance.</param>
        /// <returns>Returns the competition Id from the competition details.</returns>
        Task<Guid> SaveCompetitionAsync(CompetitionModel model);

        /// <summary>
        /// Gets the list of team details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the list of team details.</returns>
        Task<List<TeamModel>> GetTeamsAsync(Guid competitionId);

        /// <summary>
        /// Saves the team details.
        /// </summary>
        /// <param name="model"><see cref="TeamModel"/> instance.</param>
        /// <returns>Returns the team Id from the team details.</returns>
        Task<Guid> SaveTeamAsync(TeamModel model);

        /// <summary>
        /// Saves the competition-team details.
        /// </summary>
        /// <param name="model"><see cref="CompetitionTeamModel"/> instance.</param>
        /// <returns>Returns the competition-team Id from the competition-team details.</returns>
        Task<Guid> SaveCompetitionTeamAsync(CompetitionTeamModel model);
    }
}