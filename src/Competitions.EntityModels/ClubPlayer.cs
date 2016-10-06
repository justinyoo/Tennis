using System;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for club-player.
    /// </summary>
    public class ClubPlayer
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
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Club"/> instance.
        /// </summary>
        public virtual Club Club { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Player"/> instance.
        /// </summary>
        public virtual Player Player { get; set; }
    }
}