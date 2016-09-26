using System;

namespace TournamentHistory.Models
{
    /// <summary>
    /// This represents the model entity for tournament.
    /// </summary>
    public class TournamentModel
    {
        /// <summary>
        /// Gets or sets the tournament Id.
        /// </summary>
        public Guid TournamentId { get; set; }

        /// <summary>
        /// Gets or sets the tournament Key. This is given by tennis.com.au.
        /// </summary>
        public Guid TournamentKey { get; set; }

        /// <summary>
        /// Gets or sets the title of the tournament.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the summary of the tournament.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the date when the feed was published.
        /// </summary>
        public DateTimeOffset DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the player number assigned in the tournament.
        /// </summary>
        public int PlayerNumber { get; set; }
    }
}