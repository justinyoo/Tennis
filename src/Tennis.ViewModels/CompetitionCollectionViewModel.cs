using System.Collections.Generic;

using Competitions.Models;

namespace Tennis.ViewModels
{
    /// <summary>
    /// This represents the view model entity for competition collection.
    /// </summary>
    public class CompetitionCollectionViewModel
    {
        /// <summary>
        /// Gets or sets the list of competitions.
        /// </summary>
        public List<CompetitionModel> Competitions { get; set; }
    }
}
