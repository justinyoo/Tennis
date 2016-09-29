using System;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for district.
    /// </summary>
    public class DistrictModel
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
    }
}