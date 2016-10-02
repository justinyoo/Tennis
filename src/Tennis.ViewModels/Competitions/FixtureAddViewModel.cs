using System;
using System.Collections.Generic;

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
        public string CompetitionName { get; set; }

        /// <summary>
        /// Gets or sets the list of venues.
        /// </summary>
        public List<SelectListItem> Venues { get; set; }

        /// <summary>
        /// Gets or sets the venue details.
        /// </summary>
        public Guid Venue { get; set; }

        /// <summary>
        /// Gets or sets the week number that the fixture is scheduled.
        /// </summary>
        public int? Week { get; set; }

        /// <summary>
        /// Gets or sets the date when the fixture is scheduled.
        /// </summary>
        public DateTimeOffset DateScheduled { get; set; }
    }
}
