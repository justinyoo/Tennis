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
    /// This represents the service entity for teams.
    /// </summary>
    public class TeamService : ITeamService
    {
        private readonly ICompetitionDbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="TeamService"/> class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICompetitionDbContext"/> instance.</param>
        /// <param name="mapperFactory"><see cref="IMapperFactory"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContext"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="mapperFactory"/> is <see langword="null" />.</exception>
        public TeamService(ICompetitionDbContext dbContext, IMapperFactory mapperFactory)
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
        /// Gets the list of teams belong to a club.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of teams belong to a club.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clubId"/> is <see langword="null" />.</exception>
        public async Task<List<TeamModel>> GetTeamsByClubIdAsync(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(clubId));
            }

            var results = await this._dbContext.Teams
                                    .Include(p => p.Club)
                                    .Include(p => p.Club.Venue)
                                    .Include(p => p.TeamPlayers)
                                    .Include(p => p.TeamPlayers.Select(q => q.Player))
                                    .Where(p => p.ClubId == clubId)
                                    .OrderBy(p => p.Name)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<TeamToTeamModelMapper>())
            {
                var teams = mapper.Map<List<TeamModel>>(results);

                return teams;
            }
        }

        /// <summary>
        /// Gets the list of teams belong to a competition.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the list of teams belong to a competition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="competitionId"/> is <see langword="null" />.</exception>
        public async Task<List<TeamModel>> GetTeamsByCompetitionIdAsync(Guid competitionId)
        {
            if (competitionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(competitionId));
            }

            var results = await this._dbContext.Teams
                                    .Include(p => p.Club)
                                    .Include(p => p.Club.Venue)
                                    .Include(p => p.TeamPlayers)
                                    .Include(p => p.TeamPlayers.Select(q => q.Player))
                                    .Where(p => p.CompetitionId == competitionId)
                                    .OrderBy(p => p.Name)
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<TeamToTeamModelMapper>())
            {
                var teams = mapper.Map<List<TeamModel>>(results);

                return teams;
            }
        }

        /// <summary>
        /// Gets the team details.
        /// </summary>
        /// <param name="teamId">Team Id.</param>
        /// <returns>Returns the list of teams belong to a competition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="teamId"/> is <see langword="null" />.</exception>
        public async Task<TeamModel> GetTeamAsync(Guid teamId)
        {
            if (teamId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(teamId));
            }

            var result = await this._dbContext.Teams
                                   .Include(p => p.Club)
                                   .Include(p => p.Club.Venue)
                                   .Include(p => p.TeamPlayers)
                                   .Include(p => p.TeamPlayers.Select(q => q.Player))
                                   .SingleOrDefaultAsync(p => p.TeamId == teamId)
                                   .ConfigureAwait(false);

            using (var mapper = this._mapperFactory.Get<TeamToTeamModelMapper>())
            {
                var team = mapper.Map<TeamModel>(result);

                return team;
            }
        }

        /// <summary>
        /// Saves the team details.
        /// </summary>
        /// <param name="model"><see cref="TeamModel"/> instance.</param>
        /// <returns>Returns the team Id from team details.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SaveTeamAsync(TeamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var team = await this.GetOrCreateTeamAsync(model).ConfigureAwait(false);

            var transaction = this._dbContext.Database.BeginTransaction();
            try
            {
                await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return team.TeamId;
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

        private async Task<Team> GetOrCreateTeamAsync(TeamModel model)
        {
            var team = await this._dbContext.Teams
                                 .SingleOrDefaultAsync(p => p.TeamId == model.TeamId)
                                 .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (team == null)
            {
                team = new Team() { TeamId = Guid.NewGuid(), DateCreated = now };
            }

            team.ClubId = model.ClubId;
            team.CompetitionId = model.CompetitionId.GetValueOrDefault() == Guid.Empty ? null : model.CompetitionId;
            team.Name = model.Name;
            team.Tag = model.Tag;
            team.DateUpdated = now;

            var teamPlayers = new List<TeamPlayer>();
            foreach (var tp in model.TeamPlayers)
            {
                var teamPlayer = await this.GetOrCreateTeamPlayerAsync(tp).ConfigureAwait(false);
                teamPlayers.Add(teamPlayer);
            }

            team.TeamPlayers = teamPlayers;

            this._dbContext.Teams.AddOrUpdate(team);

            return team;
        }

        private async Task<TeamPlayer> GetOrCreateTeamPlayerAsync(TeamPlayerModel model)
        {
            var tp = await this._dbContext.TeamPlayers
                               .SingleOrDefaultAsync(p => p.TeamPlayerId == model.TeamPlayerId)
                               .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (tp == null)
            {
                tp = new TeamPlayer() { TeamPlayerId = Guid.NewGuid(), DateCreated = now };
            }

            tp.TeamId = model.TeamId;
            tp.PlayerId = model.PlayerId;
            tp.Order = model.Order;
            tp.DateUpdated = now;

            var player = await this.GetOrCreatePlayerAsync(model.Player).ConfigureAwait(false);

            tp.Player = player;

            this._dbContext.TeamPlayers.AddOrUpdate(tp);

            return tp;
        }

        private async Task<Player> GetOrCreatePlayerAsync(PlayerModel model)
        {
            var player = await this._dbContext.Players
                                   .SingleOrDefaultAsync(p => p.PlayerId == model.PlayerId)
                                   .ConfigureAwait(false);

            var now = DateTimeOffset.Now;

            if (player == null)
            {
                player = new Player() { PlayerId = Guid.NewGuid(), DateCreated = now };
            }

            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.DateUpdated = now;

            this._dbContext.Players.AddOrUpdate(player);

            return player;
        }
    }
}
