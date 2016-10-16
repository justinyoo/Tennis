using System;
using System.Collections.Generic;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the JSON view model entity for team.
    /// </summary>
    public class TeamJsonViewModel
    {
        /// <summary>
        /// Gets or sets the team Id.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Gets or sets the team code from trols.org.au.
        /// </summary>
        public string TeamCode { get; set; }

        /// <summary>
        /// Gets or sets the list of fixture details.
        /// </summary>
        public List<FixtureJsonViewModel> Fixtures { get; set; }
    }
}