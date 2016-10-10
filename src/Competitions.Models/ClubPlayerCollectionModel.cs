using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the model entity for player collection.
    /// </summary>
    public class ClubPlayerCollectionModel
    {
        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the list of club-player details.
        /// </summary>
        public List<ClubPlayerModel> ClubPlayers { get; set; }
    }
}