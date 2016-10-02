using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for match.
    /// </summary>
    public class MatchModel
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
        /// Gets or sets the set number;
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
        /// Gets or sets the list of match-players details.
        /// </summary>
        public List<MatchPlayerModel> MatchPlayers { get; set; }
    }
}