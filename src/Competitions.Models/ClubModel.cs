using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for tennis club.
    /// </summary>
    public class ClubModel
    {
        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the venue Id.
        /// </summary>
        public Guid VenueId { get; set; }

        /// <summary>
        /// Gets or sets the name of the club.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the manager of the club.
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the club.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the club.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email of the club.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the list of contacts.
        /// </summary>
        public List<string> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the URL of club.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the club tag used in competition.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the venue details.
        /// </summary>
        public VenueModel Venue { get; set; }

        /// <summary>
        /// Gets or sets the list of player details.
        /// </summary>
        public List<PlayerModel> Players { get; set; }
    }
}