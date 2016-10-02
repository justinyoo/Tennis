using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for match-player.
    /// </summary>
    public class MatchPlayerModel
    {
        /// <summary>
        /// Gets or sets the match player Id.
        /// </summary>
        public Guid MatchPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the match Id.
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid? PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the value that specifies whether the player belongs to home or away.
        /// </summary>
        public string HomeOrAway { get; set; }

        /// <summary>
        /// Gets or sets the player details.
        /// </summary>
        public PlayerModel Player { get; set; }
    }
}