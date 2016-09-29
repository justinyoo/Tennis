using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for competition.
    /// </summary>
    public class Competition
    {
        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the district Id where the competition belongs.
        /// </summary>
        public Guid DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the name of competition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the competition page URL at trols.org.au.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.District"/> instances.
        /// </summary>
        public virtual District District { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Fixture"/> instances.
        /// </summary>
        public virtual List<Fixture> Fixtures { get; set; }
    }
}