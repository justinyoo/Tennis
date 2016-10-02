using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for match.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Gets or sets the match Id.
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Gets or sets the fixture Id.
        /// </summary>
        public Guid FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the set number.
        /// </summary>
        public int SetNumber { get; set; }

        /// <summary>
        /// Gets or sets the set score for home team.
        /// </summary>
        public int HomeSetScore { get; set; }

        /// <summary>
        /// Gets or sets the set score for away team.
        /// </summary>
        public int AwaySetScore { get; set; }

        /// <summary>
        /// Gets or sets the game score for home team.
        /// </summary>
        public int HomeGameScore { get; set; }

        /// <summary>
        /// Gets or sets the game score for away team.
        /// </summary>
        public int AwayGameScore { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Fixture"/> instance.
        /// </summary>
        public virtual Fixture Fixture { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="MatchPlayer"/> instances.
        /// </summary>
        public virtual List<MatchPlayer> MatchPlayers { get; set; }
    }
}