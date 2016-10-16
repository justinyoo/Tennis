using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

using Competitions.EntityModels;
using Competitions.Mappers;
using Competitions.Models;

using Tennis.Common.Blob;
using Tennis.Common.Extensions;
using Tennis.Mappers;

namespace Competitions.Services
{
    /// <summary>
    /// This represents the service entity for fixtures.
    /// </summary>
    public class FixtureService : IFixtureService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IBlobContainerContext _blobContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="FixtureService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="blobContext"><see cref="IBlobContainerContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="blobContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public FixtureService(ICompetitionDbContext dbContext, IBlobContainerContext blobContext, IMapperFactory mapperFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;

            if (blobContext == null)
            {
                throw new ArgumentNullException(nameof(blobContext));
            }

            this._blobContext = blobContext;

            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }

            this._mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the list of fixtures.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the list of fixtures.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="competitionId"/> is <see langword="null" />.</exception>
        public async Task<List<FixtureModel>> GetFixturesAsync(Guid competitionId)
        {
            if (competitionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(competitionId));
            }

            var results = await this._dbContext.Fixtures
                                    .Include(p => p.Club)
                                    .Include(p => p.Club.Venue)
                                    .Include(p => p.Matches)
                                    .Include(p => p.Matches.Select(q => q.MatchPlayers))
                                    .Include(p => p.Matches.Select(q => q.MatchPlayers.Select(r => r.Player)))
                                    .Where(p => p.CompetitionId == competitionId)
                                    .OrderBy(p => p.Week)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<FixtureToFixtureModelMapper>())
            {
                var fixtures = mapper.Map<List<FixtureModel>>(results);

                return fixtures;
            }
        }

        /// <summary>
        /// Gets the list of fixtures by team.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="teamId">Team Id.</param>
        /// <returns>Returns the list of fixtures by team.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="competitionId"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="teamId"/> is <see langword="null" />.</exception>
        public async Task<List<FixtureModel>> GetFixturesByTeamIdAsync(Guid competitionId, Guid teamId)
        {
            if (competitionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(competitionId));
            }

            var fixtures = await this.GetFixturesAsync(competitionId).ConfigureAwait(false);

            if (teamId == Guid.Empty)
            {
                return fixtures;
            }

            return fixtures.Where(p => p.HomeTeamId == teamId || p.AwayTeamId == teamId).ToList();
        }

        /// <summary>
        /// Gets the fixture details.
        /// </summary>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <returns>Returns the fixture details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fixtureId"/> is <see langword="null" />.</exception>
        public async Task<FixtureModel> GetFixtureAsync(Guid fixtureId)
        {
            if (fixtureId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(fixtureId));
            }

            var result = await this._dbContext.Fixtures
                                   .Include(p => p.Club)
                                   .Include(p => p.Club.Venue)
                                   .Include(p => p.HomeTeam)
                                   .Include(p => p.HomeTeam.Club)
                                   .Include(p => p.AwayTeam)
                                   .Include(p => p.AwayTeam.Club)
                                   .Include(p => p.Matches)
                                   .Include(p => p.Matches.Select(q => q.MatchPlayers))
                                   .Include(p => p.Matches.Select(q => q.MatchPlayers.Select(r => r.Player)))
                                   .SingleOrDefaultAsync(p => p.FixtureId == fixtureId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<FixtureToFixtureModelMapper>())
            {
                var venue = mapper.Map<FixtureModel>(result);

                return venue;
            }
        }

        /// <summary>
        /// Saves the fixture details.
        /// </summary>
        /// <param name="model"><see cref="FixtureModel"/> instance.</param>
        /// <returns>Returns the fixture Id from the fixture details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SaveFixtureAsync(FixtureModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var fixture = await this.GetOrCreateFixtureAsync(model).ConfigureAwait(false);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return fixture.FixtureId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Gets the list of matches.
        /// </summary>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <returns>Returns the list of matches.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fixtureId"/> is <see langword="null" />.</exception>
        public async Task<List<MatchModel>> GetMatchesAsync(Guid fixtureId)
        {
            if (fixtureId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(fixtureId));
            }

            var results = await this._dbContext.Matches
                                    .Include(p => p.MatchPlayers)
                                    .Include(p => p.MatchPlayers.Select(q => q.Player))
                                    .Where(p => p.FixtureId == fixtureId)
                                    .OrderBy(p => p.SetNumber)
                                    .ToListAsync()
                                    .ConfigureAwait(false);


            using (var mapper = this._mapperFactory.Get<MatchToMatchModelMapper>())
            {
                var matches = mapper.Map<List<MatchModel>>(results);

                return matches;
            }
        }

        /// <summary>
        /// Uploads score sheet to Azure Blob Storage.
        /// </summary>
        /// <param name="request"><see cref="BlobUploadRequest"/> instance.</param>
        /// <returns>Returns the <see cref="BlobUploadResponse"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="request"/> is <see langword="null" />.</exception>
        public async Task<BlobUploadResponse> UploadScoreSheetAsync(BlobUploadRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = await this._blobContext.UploadAsync(request).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private async Task<Fixture> GetOrCreateFixtureAsync(FixtureModel model)
        {
            var fixture = await this._dbContext.Fixtures
                                    .SingleOrDefaultAsync(p => p.FixtureId == model.FixtureId)
                                    .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (fixture == null)
            {
                fixture = new Fixture() { FixtureId = Guid.NewGuid(), DateCreated = now };
            }

            if (model.CompetitionId != Guid.Empty)
            {
                fixture.CompetitionId = model.CompetitionId;
            }
            if (model.ClubId != Guid.Empty)
            {
                fixture.ClubId = model.ClubId;
            }
            if (model.Week > 0)
            {
                fixture.Week = model.Week;
            }
            if (model.DateScheduled > DateTimeOffset.MinValue)
            {
                fixture.DateScheduled = model.DateScheduled;
            }
            if (model.HomeTeamId != Guid.Empty)
            {
                fixture.HomeTeamId = model.HomeTeamId;
            }
            if (model.AwayTeamId != Guid.Empty)
            {
                fixture.AwayTeamId = model.AwayTeamId;
            }
            if (!string.IsNullOrWhiteSpace(model.ScoreSheet))
            {
                fixture.ScoreSheet = model.ScoreSheet;
            }
            fixture.DateUpdated = now;

            if (!model.Matches.IsNullOrEmpty())
            {
                foreach (var match in model.Matches)
                {
                    await this.GetOrCreateMatchAsync(match).ConfigureAwait(false);
                }
            }

            this._dbContext.Fixtures.AddOrUpdate(fixture);

            return fixture;
        }

        private async Task<Match> GetOrCreateMatchAsync(MatchModel model)
        {
            var match = await this._dbContext.Matches
                                  .SingleOrDefaultAsync(p => p.MatchId == model.MatchId)
                                  .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (match == null)
            {
                match = new Match() { MatchId = Guid.NewGuid(), DateCreated = now };
            }

            match.FixtureId = model.FixtureId;
            match.SetNumber = model.SetNumber;
            match.HomeSetScore = model.HomeSetScore;
            match.HomeGameScore = model.HomeGameScore;
            match.AwayGameScore = model.AwayGameScore;
            match.AwaySetScore = model.AwaySetScore;
            match.DateUpdated = now;

            this._dbContext.Matches.AddOrUpdate(match);

            return match;
        }
    }
}
