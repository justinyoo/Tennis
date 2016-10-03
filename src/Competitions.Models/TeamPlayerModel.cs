using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the model entity for team-player.
    /// </summary>
    public class TeamPlayerModel
    {
        /// <summary>
        /// Gets or sets the team player Id.
        /// </summary>
        public Guid TeamPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the team Id.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Gets or sets the order of players.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the player details.
        /// </summary>
        public PlayerModel Player { get; set; }
    }
}