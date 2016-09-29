using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for tennis club.
    /// </summary>
    public class Club
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
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.Venue"/> instance.
        /// </summary>
        public virtual Venue Venue { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Player"/> instances.
        /// </summary>
        public virtual List<Player> Players { get;set; }
    }
}