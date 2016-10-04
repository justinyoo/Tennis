using System;
using System.Collections.Generic;

using Competitions.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for team.
    /// </summary>
    public class TeamViewModel
    {
        /// <summary>
        /// Gets or sets the team details.
        /// </summary>
        public TeamModel Team { get; set; }

        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the team tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the list of competitions.
        /// </summary>
        public List<SelectListItem> Competitions { get; set; }

        /// <summary>
        /// Gets or sets the competition details.
        /// </summary>
        public Guid Competition { get; set; }
    }
}