using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="TeamService"/> class.
    /// </summary>
    public interface ITeamService : IDisposable
    {
        /// <summary>
        /// Gets the list of teams belong to a club.
        /// </summary>
        /// <returns>Returns the list of teams belong to a club.</returns>
        Task<List<TeamModel>> GetTeamsByClubIdAsync(Guid clubId);

        /// <summary>
        /// Gets the list of teams belong to a competition.
        /// </summary>
        /// <returns>Returns the list of teams belong to a competition.</returns>
        Task<List<TeamModel>> GetTeamsByCompetitionIdAsync(Guid competitionId);

        /// <summary>
        /// Gets the team details.
        /// </summary>
        /// <param name="teamId">Team Id.</param>
        /// <returns>Returns the list of teams belong to a competition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="teamId"/> is <see langword="null" />.</exception>
        Task<TeamModel> GetTeamAsync(Guid teamId);

        /// <summary>
        /// Saves the team details.
        /// </summary>
        /// <param name="model"><see cref="TeamModel"/> instance.</param>
        /// <returns>Returns the team Id from team details.</returns>
        Task<Guid> SaveTeamAsync(TeamModel model);
    }
}