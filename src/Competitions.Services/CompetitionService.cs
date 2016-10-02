using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Competitions.EntityModels;
using Competitions.Mappers;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Services
{
    /// <summary>
    /// This represents the service entity for competitions.
    /// </summary>
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public CompetitionService(ICompetitionDbContext dbContext, IMapperFactory mapperFactory)
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
        /// Gets the list of competitions.
        /// </summary>
        /// <returns>Returns the list of competitions.</returns>
        public async Task<List<CompetitionModel>> GetCompetitionsAsync()
        {
            var results = await this._dbContext.Competitions
                                    .Include(p => p.District)
                                    .OrderByDescending(p => p.Year)
                                    .ThenBy(p => p.Season)
                                    .ThenBy(p => p.Name)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<CompetitionToCompetitionModelMapper>())
            {
                var competitions = mapper.Map<List<CompetitionModel>>(results);

                return competitions;
            }
        }

        /// <summary>
        /// Gets the competition details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the competition details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="competitionId"/> is <see langword="null" />.</exception>
        public async Task<CompetitionModel> GetCompetitionAsync(Guid competitionId)
        {
            if (competitionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(competitionId));
            }

            var result = await this._dbContext.Competitions
                                   .Include(p => p.District)
                                   .Include(p => p.CompetitionClubs.Select(q => q.Club))
                                   .Include(p => p.Fixtures)
                                   .Include(p => p.Fixtures.Select(q => q.Venue))
                                   .SingleOrDefaultAsync(p => p.CompetitionId == competitionId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<CompetitionToCompetitionModelMapper>())
            {
                var competition = mapper.Map<CompetitionModel>(result);

                return competition;
            }
        }

        /// <summary>
        /// Saves the competition details.
        /// </summary>
        /// <param name="model"><see cref="CompetitionModel"/> instance.</param>
        /// <returns>Returns the competition Id from the competition details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SaveCompetitionAsync(CompetitionModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var competition = await this.GetOrCreateCompetitionAsync(model).ConfigureAwait(false);

            this._dbContext.Competitions.AddOrUpdate(competition);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return competition.CompetitionId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        /// <summary>
        /// Gets the list of competition-club details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the list of competition-club details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="competitionId"/> is <see langword="null" />.</exception>
        public async Task<List<CompetitionClubModel>> GetCompetitionClubsAsync(Guid competitionId)
        {
            if (competitionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(competitionId));
            }

            var results = await this._dbContext.CompetitionClubs
                               .Include(p => p.Club)
                               .Where(p => p.CompetitionId == competitionId)
                               .OrderBy(p => p.Club.Name)
                               .ToListAsync()
                               .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<CompetitionClubToCompetitionClubModelMapper>())
            {
                var ccs = mapper.Map<List<CompetitionClubModel>>(results);

                return ccs;
            }
        }

        /// <summary>
        /// Saves the competition-club details.
        /// </summary>
        /// <param name="model"><see cref="CompetitionClubModel"/> instance.</param>
        /// <returns>Returns the competition-club Id from the competition-club details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SaveCompetitionClubAsync(CompetitionClubModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var cc = await this.GetOrCreateCompetitionClubAsync(model).ConfigureAwait(false);

            this._dbContext.CompetitionClubs.AddOrUpdate(cc);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return cc.CompetitionClubId;
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

        private async Task<Competition> GetOrCreateCompetitionAsync(CompetitionModel model)
        {
            var competition = await this._dbContext.Competitions.
                                        SingleOrDefaultAsync(p => p.CompetitionId == model.CompetitionId)
                                        .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (competition == null)
            {
                competition = new Competition() { CompetitionId = Guid.NewGuid(), DateCreated = now };
            }

            competition.DistrictId = model.DistrictId;
            competition.Name = model.Name;
            competition.Year = model.Year;
            competition.Season = model.Season;
            competition.Day = model.Day;
            competition.Type = model.Type;
            competition.Grade = model.Grade;
            competition.Level = model.Level;
            competition.DateUpdated = now;

            return competition;
        }

        private async Task<CompetitionClub> GetOrCreateCompetitionClubAsync(CompetitionClubModel model)
        {
            var cc = await this._dbContext.CompetitionClubs
                               .SingleOrDefaultAsync(p => p.CompetitionId == model.CompetitionId &&
                                                          p.ClubId == model.ClubId)
                               .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (cc == null)
            {
                cc = new CompetitionClub() { CompetitionClubId = Guid.NewGuid(), DateCreated = now };
            }

            cc.CompetitionId = model.CompetitionId;
            cc.ClubId = model.ClubId;
            cc.ClubTag = model.Tag;
            cc.DateUpdated = now;

            return cc;
        }
    }
}
