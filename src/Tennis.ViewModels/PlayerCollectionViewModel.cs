using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Tournaments.Models;

namespace Tennis.ViewModels
{
    /// <summary>
    /// This represents the view model entity for player collection.
    /// </summary>
    public class PlayerCollectionViewModel
    {
        /// <summary>
        /// Gets or sets the list of players.
        /// </summary>
        public List<PlayerModel> Players { get; set; }

        /// <summary>
        /// Gets or sets the player's tournament feed URL.
        /// </summary>
        [Display(Name = "Tournament Feed URL")]
        public string TournamentFeedUrl { get; set; }
    }
}
