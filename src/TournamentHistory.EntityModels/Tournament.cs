using System;
using System.Collections.Generic;

namespace TournamentHistory.EntityModels
{
    /// <summary>
    /// This represents the model entity for tournament.
    /// </summary>
    public class Tournament
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
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the list of the <see cref="PlayerTournament"/> instances.
        /// </summary>
        public virtual List<PlayerTournament> PlayerTournaments { get; set; }
    }
}
