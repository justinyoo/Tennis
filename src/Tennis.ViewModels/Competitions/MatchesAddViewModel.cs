using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for matches.
    /// </summary>
    public class MatchesAddViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MatchesAddViewModel"/> class.
        /// </summary>
        public MatchesAddViewModel()
        {
            this.HomePlayers = new List<Guid>();
            this.AwayPlayers = new List<Guid>();
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
        /// Gets or sets the list of clubs.
        /// </summary>
        public List<SelectListItem> Teams { get; set; }

        /// <summary>
        /// Gets or sets the number of players for a match.
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// Gets or sets the home club details.
        /// </summary>
        public Guid HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the list of home players.
        /// </summary>
        public List<Guid> HomePlayers { get; set; }

        /// <summary>
        /// Gets or sets the away club details.
        /// </summary>
        public Guid AwayTeam { get; set; }

        /// <summary>
        /// Gets or sets the list of away players.
        /// </summary>
        public List<Guid> AwayPlayers { get; set; }
    }
}
