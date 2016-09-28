﻿using TournamentHistory.Models;

namespace Tennis.ViewModels
{
    /// <summary>
    /// Gets or sets the view model entity for the tournament feed.
    /// </summary>
    public class TournamentFeedViewModel
    {
        /// <summary>
        /// Gets or sets the tournament feed details.
        /// </summary>
        public TournamentFeedModel Feed { get; set; }

        public string FeedInJson { get; set; }
    }
}