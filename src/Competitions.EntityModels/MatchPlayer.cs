using System;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for match-player.
    /// </summary>
    public class MatchPlayer
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
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the value that specifies whether the player belongs to home or away.
        /// </summary>
        public string HomeOrAway { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Match"/> instance.
        /// </summary>
        public virtual Match Match { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Player"/> instance.
        /// </summary>
        public virtual Player Player { get; set; }
    }
}