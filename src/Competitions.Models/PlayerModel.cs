﻿using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for player.
    /// </summary>
    public class PlayerModel
    {
        /// <summary>
        /// Gets or sets the player Id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the list of team-player details.
        /// </summary>
        public List<TeamPlayerModel> TeamPlayers { get; set; }
    }
}