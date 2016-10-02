using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for fixture.
    /// </summary>
    public class Fixture
    {
        /// <summary>
        /// Gets or sets the fixture Id.
        /// </summary>
        public Guid FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the venue Id.
        /// </summary>
        public Guid VenueId { get; set; }

        /// <summary>
        /// Gets or sets the week of the fixture.
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// Gets or sets the date when the fixture is scheduled.
        /// </summary>
        public DateTimeOffset DateScheduled { get; set; }

        /// <summary>
        /// Gets or sets the score sheet image path.
        /// </summary>
        public string ScoreSheet { get; set; }

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
        /// Gets or sets the <see cref="EntityModels.Venue"/> instance.
        /// </summary>
        public virtual Venue Venue { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Match"/> instance.
        /// </summary>
        public virtual List<Match> Matches { get; set; }
    }
}