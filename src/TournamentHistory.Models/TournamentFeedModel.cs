using System.Collections.Generic;

namespace TournamentHistory.Models
{
    /// <summary>
    /// This represents the model entity for the tournament details from RSS feed.
    /// </summary>
    public class TournamentFeedModel
    {
        /// <summary>
        /// Gets or sets the title of the tournament feed.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the tournament feed.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL for the tournament feed.
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of tournament feed items.
        /// </summary>
        public List<TournamentFeedItemModel> Items { get; set; }
    }
}
