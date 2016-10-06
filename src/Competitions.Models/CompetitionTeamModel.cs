using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for competition-team.
    /// </summary>
    public class CompetitionTeamModel
    {
        /// <summary>
        /// Gets or sets the competition-team Id.
        /// </summary>
        public Guid CompetitionTeamId { get; set; }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the team Id.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Gets or sets the team number.
        /// </summary>
        public int TeamNumber { get; set; }

        /// <summary>
        /// Gets or sets the competition details.
        /// </summary>
        public CompetitionModel Competition { get; set; }

        /// <summary>
        /// Gets or sets the team details.
        /// </summary>
        public TeamModel Team { get; set; }
    }
}
