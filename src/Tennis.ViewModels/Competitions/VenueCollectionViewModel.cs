using System.Collections.Generic;

using Competitions.Models;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for venue collection.
    /// </summary>
    public class VenueCollectionViewModel
    {
        /// <summary>
        /// Gets or sets the list of venues.
        /// </summary>
        public List<VenueModel> Venues { get; set; }
    }
}