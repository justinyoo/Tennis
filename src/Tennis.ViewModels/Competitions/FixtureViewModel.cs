using System;
using Competitions.Models;

using Microsoft.AspNetCore.Http;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for fixtures.
    /// </summary>
    public class FixtureViewModel
    {
        /// <summary>
        /// Gets or sets the fixture Id.
        /// </summary>
        public Guid FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the fixture details.
        /// </summary>
        public FixtureModel Fixture { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified file path for score sheet.
        /// </summary>
        public IFormFile ScoreSheet { get; set; }
    }
}