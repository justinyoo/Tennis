using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels
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
        /// Gets or sets the season of competition - Spring, Summer, Autumn, Winter.
        /// </summary>
        public string Season { get; set; }

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

        /// <summary>
        /// Gets or sets the competition page URL at trols.org.au.
        /// </summary>
        public string Url { get; set; }
    }
}