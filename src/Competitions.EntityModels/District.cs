using System;
using System.Collections.Generic;

namespace Competitions.EntityModels
{
    /// <summary>
    /// This represents the entity for district.
    /// </summary>
    public class District
    {
        /// <summary>
        /// Gets or sets the district Id.
        /// </summary>
        public Guid DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the name of district.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL of district.
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
        /// Gets or sets the list of <see cref="Competition"/> instances.
        /// </summary>
        public virtual List<Competition> Competitions { get; set; }
    }
}