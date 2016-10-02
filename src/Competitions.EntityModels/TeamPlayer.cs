using System;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for team-player.
    /// </summary>
    public class TeamPlayer
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
        /// Gets or sets the <see cref="EntityModels.Team"/> instance.
        /// </summary>
        public virtual Team Team { get; set; }
    }
}