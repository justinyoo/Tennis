using System;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for competition-team.
    /// </summary>
    public class CompetitionTeam
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
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Competition"/> instance.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Team"/> instance.
        /// </summary>
        public virtual Team Team { get; set; }
    }
}
