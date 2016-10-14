using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for fixtures.
    /// </summary>
    public class FixtureAddViewModel
    {
        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the competition name.
        /// </summary>
        [DisplayName("Competition")]
        public string CompetitionName { get; set; }

        /// <summary>
        /// Gets or sets the list of club.
        /// </summary>
        public List<SelectListItem> Clubs { get; set; }

        /// <summary>
        /// Gets or sets the club details.
        /// </summary>
        public Guid Club { get; set; }

        /// <summary>
        /// Gets or sets the week number that the fixture is scheduled.
        /// </summary>
        public int? Round { get; set; }

        /// <summary>
        /// Gets or sets the date when the fixture is scheduled.
        /// </summary>
        [DisplayName("Date Scheduled")]
        public DateTimeOffset DateScheduled { get; set; }

        /// <summary>
        /// Gets or sets the list of teams.
        /// </summary>
        public List<SelectListItem> Teams { get; set; }

        /// <summary>
        /// Gets or sets the home team details.
        /// </summary>
        public Guid HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the away team details.
        /// </summary>
        public Guid AwayTeam { get; set; }
    }
}
