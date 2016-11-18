using System;
using System.Threading.Tasks;

using Tournaments.Models;

namespace Tournaments.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="TournamentService"/> class.
    /// </summary>
    public interface ITournamentService : IDisposable
    {
        /// <summary>
        /// Gets the tournament details corresponding to the tournament Id.
        /// </summary>
        /// <param name="tournamentId">Tournament Id.</param>
        /// <returns>Returns the <see cref="TournamentModel"/> instance.</returns>
        Task<TournamentModel> GetTournamentByIdAsync(Guid tournamentId);

        /// <summary>
        /// Gets the tournament details corresponding to the tournament key.
        /// </summary>
        /// <param name="tournamentKey">Tournament key from tennis.com.au.</param>
        /// <returns>Returns the <see cref="TournamentModel"/> instance.</returns>
        Task<TournamentModel> GetTournamentByKeyAsync(Guid tournamentKey);

        /// <summary>
        /// Saves the tournament details.
        /// </summary>
        /// <param name="model"><see cref="TournamentModel"/> instance.</param>
        /// <returns>Returns the tournament Id.</returns>
        Task<Guid> SaveTournamentAsync(TournamentModel model);

        /// <summary>
        /// Saves the tournament details.
        /// </summary>
        /// <param name="tournamentKey">Tournament key.</param>
        /// <param name="model"><see cref="TournamentFeedItemModel"/> instance.</param>
        /// <returns>Returns the tournament Id.</returns>
        Task<Guid> SaveTournamentAsync(Guid tournamentKey, TournamentFeedItemModel model);

        /// <summary>
        /// Saves the tournament details.
        /// </summary>
        /// <param name="model"><see cref="PlayerTournamentFeedItemModel"/> instance.</param>
        /// <returns>Returns the tournament Id.</returns>
        Task<Guid> SaveTournamentAsync(PlayerTournamentFeedItemModel model);
    }
}