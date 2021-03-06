﻿using System;
using System.Collections.Generic;

namespace Competitions.Models
{
    /// <summary>
    /// This represents the entity for fixture.
    /// </summary>
    public class FixtureModel
    {
        /// <summary>
        /// Gets or sets the fixture Id.
        /// </summary>
        public Guid FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the clubId Id as a venue.
        /// </summary>
        public Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the competition Id.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the week of the fixture.
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// Gets or sets the date when the fixture is scheduled.
        /// </summary>
        public DateTimeOffset DateScheduled { get; set; }

        /// <summary>
        /// Gets or sets the home team Id.
        /// </summary>
        public Guid HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the away team Id.
        /// </summary>
        public Guid AwayTeamId { get; set; }

        /// <summary>
        /// Gets or sets the score sheet image path.
        /// </summary>
        public string ScoreSheet { get; set; }

        /// <summary>
        /// Gets or sets the venue details.
        /// </summary>
        public ClubModel Club { get; set; }

        /// <summary>
        /// Gets or sets the competition details.
        /// </summary>
        public CompetitionModel Competition { get; set; }

        /// <summary>
        /// Gets or sets the home team details.
        /// </summary>
        public TeamModel HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the away team details.
        /// </summary>
        public TeamModel AwayTeam { get; set; }

        /// <summary>
        /// Gets or sets the list of match details.
        /// </summary>
        public List<MatchModel> Matches { get; set; }
    }
}