using System.Collections.Generic;

using Competitions.Models;

namespace Tennis.ViewModels
{
    /// <summary>
    /// This represents the view model entity for club collection.
    /// </summary>
    public class ClubCollectionViewModel
    {
        /// <summary>
        /// Gets or sets the list of clubs.
        /// </summary>
        public List<ClubModel> Clubs { get; set; }
    }
}