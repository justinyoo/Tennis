using System;

namespace Tournaments.Models
{
    /// <summary>
    /// This represents the model entity for the tournament details from RSS feed.
    /// </summary>
    public class TournamentFeedItemModel
    {
        /// <summary>
        /// Gets or sets the feed item Id for the tournament.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Gets or sets the title of the tournament.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the summary of the tournament.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the URL for the tournament.
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the date when the feed was published.
        /// </summary>
        public DateTimeOffset DatePublished { get; set; }
    }
}