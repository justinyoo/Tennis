using System;
using System.Collections.Generic;

using Competitions.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for competition.
    /// </summary>
    public class CompetitionViewModel
    {
        /// <summary>
        /// Gets or sets the competition details.
        /// </summary>
        public CompetitionModel Competition { get; set; }

        /// <summary>
        /// Gets or sets the list of clubs.
        /// </summary>
        public List<SelectListItem> Clubs { get; set; }

        /// <summary>
        /// Gets or sets the club.
        /// </summary>
        public Guid Club { get; set; }

        /// <summary>
        /// Gets or sets the list of teams.
        /// </summary>
        public List<SelectListItem> Teams { get; set; }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        public Guid Team { get; set; }

        /// <summary>
        /// Gets or sets the team number in the competition.
        /// </summary>
        public int TeamNumber { get; set; }
    }
}