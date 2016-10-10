using System;
using System.Collections.Generic;

using Competitions.Models;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for list of players to add.
    /// </summary>
    public class PlayersAddViewModel
    {
        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public string ClubName { get; set; }

        /// <summary>
        /// Gets or sets the list of players.
        /// </summary>
        public List<PlayerModel> Players { get; set; }

        /// <summary>
        /// Gets or sets the list of first names.
        /// </summary>
        public List<string> FirstNames { get; set; }

        /// <summary>
        /// Gets or sets the list of last names.
        /// </summary>
        public List<string> LastNames { get; set; }
    }
}