using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for club-player.
    /// </summary>
    public class ClubPlayerModel
    {
        /// <summary>
        /// Gets or sets the club-player Id.
        /// </summary>
        public Guid ClubPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the club details.
        /// </summary>
        public ClubModel Club { get; set; }

        /// <summary>
        /// Gets or sets the player details.
        /// </summary>
        public PlayerModel Player { get; set; }
    }
}