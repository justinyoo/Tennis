using System;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the JSON view model entity for fixture.
    /// </summary>
    public class FixtureJsonViewModel
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
        /// Gets or sets the round.
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// Gets or sets the date when the fixture is scheduled.
        /// </summary>
        public DateTimeOffset DateScheduled { get; set; }

        /// <summary>
        /// Gets or sets the club where the fixture is held.
        /// </summary>
        public string Club { get; set; }

        /// <summary>
        /// Gets or sets the venue where the fixture is held.
        /// </summary>
        public string Venue { get; set; }
    }
}