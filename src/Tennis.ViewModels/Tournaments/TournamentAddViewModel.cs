using System;
using System.ComponentModel.DataAnnotations;

using Tournaments.Models;

namespace Tennis.ViewModels.Tournaments
{
    /// <summary>
    /// This represents the view model entity for tournament addition.
    /// </summary>
    public class TournamentAddViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentAddViewModel"/> class.
        /// </summary>
        public TournamentAddViewModel()
        {
            this.IsSummarySameAsTitle = true;
        }

        /// <summary>
        /// Gets or sets the <see cref="PlayerModel"/> instance.
        /// </summary>
        public PlayerModel Player { get; set; }

        /// <summary>
        /// Gets or sets the tournament player URL.
        /// </summary>
        [Display(Name = "Tournament Player URL")]
        public string TournamentPlayerUrl { get; set; }

        /// <summary>
        /// Gets or sets the tournament title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the tournament summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the title and summary are the same as each other or not.
        /// </summary>
        public bool IsSummarySameAsTitle { get; set; }

        /// <summary>
        /// Gets or sets the date last published.
        /// </summary>
        [Display(Name = "Date Published")]
        public DateTimeOffset DatePublished { get; set; }
    }
}
