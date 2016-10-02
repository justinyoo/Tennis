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
    /// This represents the service entity for clubs.
    /// </summary>
    public class ClubService : IClubService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="ClubService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public ClubService(ICompetitionDbContext dbContext, IMapperFactory mapperFactory)
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
        /// Gets the list of districts.
        /// </summary>
        /// <returns>Returns the list of districts.</returns>
        public async Task<List<ClubModel>> GetClubsAsync()
        {
            var results = await this._dbContext.Clubs
                                    .Include(p => p.Players)
                                    .Include(p => p.Venue)
                                    .OrderBy(p => p.Name)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<ClubToClubModelMapper>())
            {
                var clubs = mapper.Map<List<ClubModel>>(results);

                return clubs;
            }
        }

        /// <summary>
        /// Gets the club details.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the club details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clubId"/> is <see langword="null" />.</exception>
        public async Task<ClubModel> GetClubAsync(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(clubId));
            }

            var result = await this._dbContext.Clubs
                                   .Include(p => p.Players)
                                   .Include(p => p.Venue)
                                   .SingleOrDefaultAsync(p => p.ClubId == clubId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<ClubToClubModelMapper>())
            {
                var club = mapper.Map<ClubModel>(result);

                return club;
            }
        }

        /// <summary>
        /// Saves the club details.
        /// </summary>
        /// <param name="clubModel"><see cref="ClubModel"/> instance.</param>
        /// <param name="venueModel"><see cref="VenueModel"/> instance.</param>
        /// <returns>Returns the club Id from the club details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clubModel"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">At least venue Id should be provided</exception>
        public async Task<Guid> SaveClubAsync(ClubModel clubModel, VenueModel venueModel = null)
        {
            if (clubModel == null)
            {
                throw new ArgumentNullException(nameof(clubModel));
            }

            if (venueModel == null && clubModel.VenueId == Guid.Empty)
            {
                throw new ArgumentException("At least venue Id should be provided");
            }

            var club = await this.GetOrCreateClubAsync(clubModel, venueModel).ConfigureAwait(false);

            this._dbContext.Clubs.AddOrUpdate(club);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return club.ClubId;
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

        private async Task<Club> GetOrCreateClubAsync(ClubModel clubModel, VenueModel venueModel = null)
        {
            var club = await this._dbContext.Clubs
                                 .SingleOrDefaultAsync(p => p.ClubId == clubModel.ClubId)
                                 .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (club == null)
            {
                club = new Club() { ClubId = Guid.NewGuid(), DateCreated = now };
            }

            club.VenueId = clubModel.VenueId;
            club.Name = clubModel.Name;
            club.Manager = clubModel.Manager;
            club.Phone = clubModel.Phone.Replace(" ", "");
            club.Mobile = clubModel.Mobile.Replace(" ", "");
            club.Email = clubModel.Email;
            club.DateUpdated = now;

            if (venueModel == null)
            {
                return club;
            }

            var venue = await this.GetOrCreateVenueAsync(venueModel).ConfigureAwait(false);

            club.VenueId = venue.VenueId;
            club.Venue = venue;

            return club;
        }

        private async Task<Venue> GetOrCreateVenueAsync(VenueModel model)
        {
            var venue = await this._dbContext.Venues
                                  .SingleOrDefaultAsync(p => p.VenueId == model.VenueId)
                                  .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (venue == null)
            {
                venue = new Venue() { VenueId = Guid.NewGuid(), DateCreated = now };
            }

            venue.Address1 = model.Address1;
            venue.Address2 = model.Address2;
            venue.Suburb = model.Suburb;
            venue.State = model.State;
            venue.Postcode = model.Postcode;
            venue.DateUpdated = now;

            return venue;
        }
    }
}
