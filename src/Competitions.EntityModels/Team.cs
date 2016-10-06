using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for team.
    /// </summary>
    public class Team
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
        /// Gets or sets the team name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the team tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Club"/> instance.
        /// </summary>
        public virtual Club Club { get; set; }


        /// <summary>
        /// Gets or sets the list of <see cref="CompetitionTeam"/> instances.
        /// </summary>
        public virtual List<CompetitionTeam> CompetitionTeams { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="TeamPlayer"/> instance.
        /// </summary>
        public virtual List<TeamPlayer> TeamPlayers { get; set; }
    }
}
