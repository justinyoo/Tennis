using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the model entity for team.
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// Gets or sets the team Id.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid? CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the team tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the club details.
        /// </summary>
        public ClubModel Club { get; set; }

        /// <summary>
        /// Gets or sets the list of team-players.
        /// </summary>
        public List<TeamPlayerModel> TeamPlayers { get; set; }

        /// <summary>
        /// Gets or sets the list of players.
        /// </summary>
        public List<PlayerModel> Players { get; set; }
    }
}