﻿using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for fixture.
    /// </summary>
    public class FixtureModel
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
    }
}