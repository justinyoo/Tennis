using System;

namespace Tournaments.Models
{
    /// <summary>
    /// This represents the model entity for the player and tournament details from RSS feed.
    /// </summary>
    public class PlayerTournamentFeedItemModel
    {
        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TournamentFeedItemModel"/> instance.
        /// </summary>
        public TournamentFeedItemModel Item { get; set; }
    }
}