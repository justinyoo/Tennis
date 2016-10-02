using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for competition.
    /// </summary>
    public class CompetitionModel
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
        /// Gets or sets the year of competition.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the season of competition - Spring, Summer, Autumn, Winter.
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// Gets or sets the day when the competition is held - Friday, Saturday, Sunday
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets the type of competition - Boys, Girls, Open
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the grade of competition - Triples, Special
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// Gets or sets the level of competition. - D1, C1
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the official URL at trols.org.au
        /// </summary>
        public string TrolsUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of participating clubs.
        /// </summary>
        public List<ClubModel> ParticipatingClubs { get; set; }

        /// <summary>
        /// Gets or sets the list of fixtures.
        /// </summary>
        public List<FixtureModel> Fixtures { get; set; }
    }
}