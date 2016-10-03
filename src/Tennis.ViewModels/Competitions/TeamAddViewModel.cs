using System;
using System.Collections.Generic;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for team.
    /// </summary>
    public class TeamAddViewModel
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
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the team tag.
        /// </summary>
        public string Tag { get; set; }

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
