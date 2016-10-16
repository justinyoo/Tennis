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
        /// Gets or sets the official URL at trols.org.au.
        /// </summary>
        public string TrolsUrl { get; set; }

        /// <summary>
        /// Gets or sets the fixture page at trols.org.au.
        /// </summary>
        public string TrolsFixture { get; set; }

        /// <summary>
        /// Gets or sets the results page at trols.org.au.
        /// </summary>
        public string TrolsResults { get; set; }

        /// <summary>
        /// Gets or sets the ladders page at trols.org.au.
        /// </summary>
        public string TrolsLadders { get; set; }

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