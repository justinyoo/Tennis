using System;

namespace TournamentHistory.EntityModels
{
    /// <summary>
    /// This represents the model entity for player-tournament.
    /// </summary>
    public class PlayerTournament
    {
        /// <summary>
        /// Gets or sets the player-tournament Id.
        /// </summary>
        public Guid PlayerTournamentId { get; set; }

        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the tournament Id.
        /// </summary>
        public Guid TournamentId { get; set; }

        /// <summary>
        /// Gets or sets the player number assigned in the tournament.
        /// </summary>
        public int PlayerNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Player"/> instance.
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Tournament"/> instance.
        /// </summary>
        public virtual Tournament Tournament { get; set; }
    }
}
