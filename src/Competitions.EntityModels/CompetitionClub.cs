using System;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for competition-club.
    /// </summary>
    public class CompetitionClub
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
        public string ClubTag { get; set; }

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
        /// Gets or sets the <see cref="EntityModels.Club"/> instance.
        /// </summary>
        public virtual Club Club { get; set; }
    }
}
