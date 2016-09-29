using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for venue.
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// Gets or sets the venue Id.
        /// </summary>
        public Guid VenueId { get; set; }

        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Club"/> instances.
        /// </summary>
        public virtual List<Club> Clubs { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Fixture"/> instances.
        /// </summary>
        public virtual List<Fixture> Fixtures { get; set; }
    }
}