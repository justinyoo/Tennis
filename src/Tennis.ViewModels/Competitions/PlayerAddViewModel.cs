using System;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for players.
    /// </summary>
    public class PlayerAddViewModel
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
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}
