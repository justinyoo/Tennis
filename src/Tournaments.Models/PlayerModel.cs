using System;
using System.Collections.Generic;

namespace Tournaments.Models
{
    /// <summary>
    /// This represents the model entity for player.
    /// </summary>
    public class PlayerModel
    {
        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the member Id of tennis.com.au.
        /// </summary>
        public long? MemberId { get; set; }

        /// <summary>
        /// Gets or sets the profile Id of tennis.com.au.
        /// </summary>
        public Guid? ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the player Id of tennis.com.au used for ranking.
        /// </summary>
        public long? RankingId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle names.
        /// </summary>
        public string MiddleNames { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the list of tournament details.
        /// </summary>
        public List<TournamentModel> Tournaments { get; set; }
    }
}
