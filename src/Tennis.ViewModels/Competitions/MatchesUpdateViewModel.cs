using System;
using System.Collections.Generic;

using Competitions.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for matches.
    /// </summary>
    public class MatchesUpdateViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MatchesUpdateViewModel"/> class.
        /// </summary>
        public MatchesUpdateViewModel()
        {
            this.HomePlayerIds = new List<Guid>();
            this.AwayPlayerIds = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the fixture Id.
        /// </summary>
        public Guid FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the fixture details.
        /// </summary>
        public FixtureModel Fixture { get; set; }

        /// <summary>
        /// Gets or sets the list of clubs.
        /// </summary>
        public List<SelectListItem> HomePlayers { get; set; }

        /// <summary>
        /// Gets or sets the list of home players.
        /// </summary>
        public List<Guid> HomePlayerIds { get; set; }

        /// <summary>
        /// Gets or sets the list of clubs.
        /// </summary>
        public List<SelectListItem> AwayPlayers { get; set; }

        /// <summary>
        /// Gets or sets the list of away players.
        /// </summary>
        public List<Guid> AwayPlayerIds { get; set; }

        /// <summary>
        /// Gets or sets the number of players for a match.
        /// </summary>
        public int NumberOfPlayers { get; set; }
    }
}
