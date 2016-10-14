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
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the week of the fixture.
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// Gets or sets the date when the fixture is scheduled.
        /// </summary>
        public DateTimeOffset DateScheduled { get; set; }

        /// <summary>
        /// Gets or sets the home team Id.
        /// </summary>
        public Guid HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the away team Id.
        /// </summary>
        public Guid AwayTeamId { get; set; }

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
        /// Gets or sets the <see cref="EntityModels.Club"/> instance.
        /// </summary>
        public virtual Club Club { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Competition"/> instance.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Team"/> instance for home.
        /// </summary>
        public virtual Team HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Team"/> instance for away.
        /// </summary>
        public virtual Team AwayTeam { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Match"/> instance.
        /// </summary>
        public virtual List<Match> Matches { get; set; }
    }
}