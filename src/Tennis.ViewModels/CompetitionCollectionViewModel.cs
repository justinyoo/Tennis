using System.Collections.Generic;

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
        public List<CompetitionModel> Competions { get; set; }
    }

    /// <summary>
    /// This represents the model entity for competition.
    /// </summary>
    public class CompetitionModel
    {
        /// <summary>
        /// Gets or sets the name of competition.
        /// </summary>
        public string Name { get; set; }
    }
}
