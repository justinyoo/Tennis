using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

using Competitions.EntityModels;
using Competitions.Mappers;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Services
{
    /// <summary>
    /// This represents the service entity for fixtures.
    /// </summary>
    public class FixtureService : IFixtureService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="FixtureService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public FixtureService(ICompetitionDbContext dbContext, IMapperFactory mapperFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;

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
                var venues = mapper.Map<List<FixtureModel>>(results);

                return venues;
            }
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

            this._dbContext.Fixtures.AddOrUpdate(fixture);

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

            fixture.CompetitionId = model.CompetitionId;
            fixture.ClubId = model.ClubId;
            fixture.Week = model.Week;
            fixture.DateScheduled = model.DateScheduled;
            fixture.DateUpdated = now;

            return fixture;
        }
    }
}
