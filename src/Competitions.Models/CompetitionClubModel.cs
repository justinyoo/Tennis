using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for competition-club.
    /// </summary>
    public class CompetitionClubModel
    {
        /// <summary>
        /// Gets or sets the competition-club Id.
        /// </summary>
        public Guid CompetitionClubId { get; set; }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the club tag, if multiple teams from one club participate in the competition.
        /// </summary>
        public string Tag { get; set; }
    }
}
