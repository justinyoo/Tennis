using System;
using System.Collections.Generic;

using Competitions.Models;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for fixtures edit.
    /// </summary>
    public class MatchResultsUpdateViewModel
    {
        /// <summary>
        /// Gets or sets the fixture Id.
        /// </summary>
        public Guid FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the fixture details.
        /// </summary>
        public FixtureModel Fixture { get; set; }

        /// <summary>
        /// Gets or sets the list of match Ids.
        /// </summary>
        public List<Guid> MatchIds { get; set; }

        /// <summary>
        /// Gets or sets the list of set numbers.
        /// </summary>
        public List<int> SetNumbers { get; set; }

        /// <summary>
        /// Gets or sets the list of home set scores.
        /// </summary>
        public List<int> HomeSetScores { get; set; }

        /// <summary>
        /// Gets or sets the list of home game scores.
        /// </summary>
        public List<int> HomeGameScores { get; set; }

        /// <summary>
        /// Gets or sets the list of away game scores.
        /// </summary>
        public List<int> AwayGameScores { get; set; }

        /// <summary>
        /// Gets or sets the list of away set scores.
        /// </summary>
        public List<int> AwaySetScores { get; set; }
    }
}