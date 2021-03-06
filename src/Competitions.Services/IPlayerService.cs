using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="PlayerService"/> class.
    /// </summary>
    public interface IPlayerService : IDisposable
    {
        /// <summary>
        /// Gets the list of players belong to a club.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of players belong to the club.</returns>
        Task<List<PlayerModel>> GetPlayersByClubIdAsync(Guid clubId);

        /// <summary>
        /// Gets the list of players belong to a team..
        /// </summary>
        /// <param name="teamId">Team Id.</param>
        /// <returns>Returns the list of players belong to the team.</returns>
        Task<List<PlayerModel>> GetPlayersByTeamIdAsync(Guid teamId);

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the player details.</returns>
        Task<PlayerModel> GetPlayerAsync(Guid playerId);

        /// <summary>
        /// Saves the player details.
        /// </summary>
        /// <param name="model"><see cref="PlayerModel"/> instance.</param>
        /// <returns>Returns the player Id from the player details.</returns>
        Task<Guid> SavePlayerAsync(PlayerModel model);

        /// <summary>
        /// Saves the club-players details.
        /// </summary>
        /// <param name="model"><see cref="ClubPlayerCollectionModel"/> instance.</param>
        /// <returns>Returns the list of club-player Id from the club-player details.</returns>
        Task<List<Guid>> SaveClubPlayersAsync(ClubPlayerCollectionModel model);

        /// <summary>
        /// Saves match details.
        /// </summary>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <param name="homePlayers">List of home players.</param>
        /// <param name="awayPlayers">List of away players.</param>
        /// <returns>Returns the list of matches.</returns>
        Task<List<Guid>> SaveMatchesAsync(Guid fixtureId, List<Guid> homePlayers, List<Guid> awayPlayers);
    }
}