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
        /// Gets the list of players.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of clubs.</returns>
        Task<List<PlayerModel>> GetPlayersAsync(Guid clubId);

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
    }
}