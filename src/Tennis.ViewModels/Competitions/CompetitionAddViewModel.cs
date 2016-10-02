using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for competition to add.
    /// </summary>
    public class CompetitionAddViewModel
    {
        /// <summary>
        /// Gets or sets the list of districts.
        /// </summary>
        [DisplayName("Districts")]
        public List<SelectListItem> Districts { get; set; }

        /// <summary>
        /// Gets or sets the district where the competition belongs.
        /// </summary>
        public Guid District { get; set; }

        /// <summary>
        /// Gets or sets the name of competition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the year of competition.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the list of seasons.
        /// </summary>
        public List<string> Seasons { get; set; }

        /// <summary>
        /// Gets or sets the season of competition - Spring, Summer, Autumn, Winter.
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// Gets or sets the list of days.
        /// </summary>
        public List<string> Days { get; set; }

        /// <summary>
        /// Gets or sets the day when the competition is held - Friday, Saturday, Sunday
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets the list of types.
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// Gets or sets the type of competition - Boys, Girls, Open
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the grade of competition - Triples, Special
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// Gets or sets the level of competition. - D1, C1
        /// </summary>
        public string Level { get; set; }
    }
}