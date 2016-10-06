using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ClubPlayer"/> instances.
        /// </summary>
        public virtual List<ClubPlayer> ClubPlayers { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="MatchPlayer"/> instances.
        /// </summary>
        public virtual List<MatchPlayer> MatchPlayers { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="TeamPlayer"/> instances.
        /// </summary>
        public virtual List<TeamPlayer> TeamPlayers { get; set; }
    }
}