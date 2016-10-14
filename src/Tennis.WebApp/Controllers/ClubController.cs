using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Competitions.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Tennis.ViewModels.Competitions;
using Tennis.WebApp.Mappers;
using Tennis.WebApp.ServiceContexts;

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for clubs.
    /// </summary>
    [Route("clubs")]
    public class ClubController : Controller
    {
        private readonly IClubServiceContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="ClubController"/> class.
        /// </summary>
        /// <param name="context"><see cref="IClubServiceContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public ClubController(IClubServiceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the list of clubs.
        /// </summary>
        /// <returns>Returns the list of clubs.</returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetClubs()
        {
            var clubs = await this._context.ClubService
                                  .GetClubsAsync()
                                  .ConfigureAwait(false);

            var vm = new ClubCollectionViewModel() { Clubs = clubs };

            return View("Index", vm);
        }

        /// <summary>
        /// Gets the club details.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the club details.</returns>
        [Route("{clubId}")]
        [HttpGet]
        public async Task<IActionResult> GetClub(Guid clubId)
        {
            var club = await this._context.ClubService
                                 .GetClubAsync(clubId)
                                 .ConfigureAwait(false);

            var vm = new ClubViewModel() { Club = club };

            return View("GetClub", vm);
        }

        /// <summary>
        /// Adds club details.
        /// </summary>
        /// <returns>Returns the club details input form.</returns>
        [Route("add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddClub()
        {
            var items = await this._context.VenueService.GetStatesAsync().ConfigureAwait(false);
            var states = items.Select(p => new SelectListItem() { Text = p, Value = p }).ToList();
            states.Insert(0, new SelectListItem() { Text = "Select State", Value = string.Empty, Selected = true });

            var vm = new ClubAddViewModel() { States = states };

            return View("AddClub", vm);
        }

        /// <summary>
        /// Adds club details.
        /// </summary>
        /// <param name="model"><see cref="ClubAddViewModel"/> instance.</param>
        /// <returns>Returns the club details.</returns>
        [Route("add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClub(ClubAddViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var club = this._context.Map<ClubAddViewModelToClubModelMapper, ClubModel>(model);

            var clubId = await this._context.ClubService.SaveClubAsync(club).ConfigureAwait(false);

            return RedirectToAction("GetClub", new { clubId = clubId });
        }

        /// <summary>
        /// Gets the list of teams in a club.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of teams in a club.</returns>
        [Route("{clubId}/teams")]
        [HttpGet]
        public async Task<IActionResult> GetTeams(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            var teams = await this._context.TeamService.GetTeamsByClubIdAsync(clubId).ConfigureAwait(false);

            return new JsonResult(teams.Select(p => new { name = $"{p.Name} {p.Tag}", teamId = p.TeamId}));
        }

        /// <summary>
        /// Gets the team details.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <param name="teamId">Team Id.</param>
        /// <returns>Returns the team details.</returns>
        [Route("{clubId}/teams/{teamId}")]
        [HttpGet]
        public async Task<IActionResult> GetTeam(Guid clubId, Guid teamId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            if (teamId == Guid.Empty)
            {
                return NotFound();
            }

            var team = await this._context.TeamService.GetTeamAsync(teamId).ConfigureAwait(false);

            var playerItems = await this._context.PlayerService.GetPlayersByClubIdAsync(clubId).ConfigureAwait(false);
            var players = this._context.Map<PlayerModelToSelectListItemMapper, List<SelectListItem>>(playerItems);
            players.Insert(0, new SelectListItem() { Text = "Select Player", Value = string.Empty, Selected = true });

            var competitionItems = await this._context.CompetitionService.GetCompetitionsAsync().ConfigureAwait(false);
            var competitions = this._context.Map<CompetitionModelToSelectListItemMapper, List<SelectListItem>>(competitionItems);
            competitions.Insert(0, new SelectListItem() { Text = "Select Competition", Value = string.Empty, Selected = true });

            var vm = new TeamViewModel() { Team = team, Players = players, Competitions = competitions };

            return View("GetTeam", vm);
        }

        /// <summary>
        /// Adds the team.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the team input form.</returns>
        [Route("{clubId}/teams/add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddTeam(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            var club = await this._context.ClubService.GetClubAsync(clubId).ConfigureAwait(false);
            var players = await this._context.PlayerService.GetPlayersByClubIdAsync(clubId).ConfigureAwait(false);

            var vm = new TeamAddViewModel() { ClubId = clubId, ClubName = club.Name, Players = players };

            return View("AddTeam", vm);
        }

        /// <summary>
        /// Adds the team.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <param name="model"><see cref="TeamAddViewModel"/> instance.</param>
        /// <returns>Returns the team details.</returns>
        [Route("{clubId}/teams/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam(Guid clubId, TeamAddViewModel model)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var team = this._context.Map<TeamAddViewModelToTeamModelMapper, TeamModel>(model);
            team.ClubId = clubId;

            var teamId = await this._context.TeamService.SaveTeamAsync(team).ConfigureAwait(false);

            return RedirectToAction("GetTeam", new { clubId = clubId, teamId = teamId });
        }

        /// <summary>
        /// Gets the list of players belong to the club.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of players belong to the club.</returns>
        [Route("{clubId}/players")]
        [HttpGet]
        public async Task<IActionResult> GetPlayers(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            var players = await this._context.PlayerService.GetPlayersByClubIdAsync(clubId).ConfigureAwait(false);

            return new JsonResult(players.Select(p => new { name = $"{p.FirstName} {p.LastName}", playerId = p.PlayerId }));
        }

        /// <summary>
        /// Adds list of players to the club.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the players add input form page.</returns>
        [Route("{clubId}/players/add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddPlayers(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            var club = await this._context.ClubService.GetClubAsync(clubId).ConfigureAwait(false);
            var players = await this._context.PlayerService.GetPlayersByClubIdAsync(clubId).ConfigureAwait(false);

            var vm = new PlayersAddViewModel() { ClubId = clubId, ClubName = club.Name, Players = players };

            return View("AddPlayers", vm);
        }

        /// <summary>
        /// Adds list of players to the club..
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <param name="model"><see cref="PlayersAddViewModel"/> instance.</param>
        /// <returns>Returns the club page.</returns>
        [Route("{clubId}/players/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlayers(Guid clubId, PlayersAddViewModel model)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var collection = this._context.Map<PlayersAddViewModelToClubPlayerCollectionModelMapper, ClubPlayerCollectionModel>(model);
            collection.ClubId = clubId;

            await this._context.PlayerService.SaveClubPlayersAsync(collection).ConfigureAwait(false);

            return RedirectToAction("GetClub", new { clubId = clubId });
        }

        /// <summary>
        /// Add players to the team.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <param name="teamId">Team Id.</param>
        /// <param name="model"><see cref="TeamViewModel"/> instance.</param>
        /// <returns>Returns the team details with player allocated.</returns>
        [Route("{clubId}/teams/{teamId}/players/add")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPlayerToTeam(Guid clubId, Guid teamId, TeamViewModel model)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            if (teamId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var team = this._context.Map<TeamViewModelToTeamModelMapper, TeamModel>(model);
            team.ClubId = clubId;
            team.TeamId = teamId;

            await this._context.TeamService.SaveTeamAsync(team).ConfigureAwait(false);

            return RedirectToAction("GetTeam", new { clubId = clubId, teamId = teamId });
        }

        /// <summary>
        /// Adds competitions to the team.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <param name="teamId">Team Id.</param>
        /// <param name="model"><see cref="TeamViewModel"/> instance.</param>
        /// <returns>Returns the team details with competition allocated.</returns>
        [Route("{clubId}/teams/{teamId}/competitions/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompetition(Guid clubId, Guid teamId, TeamViewModel model)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            if (teamId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var team = this._context.Map<TeamViewModelToTeamModelMapper, TeamModel>(model);
            team.ClubId = clubId;
            team.TeamId = teamId;

            await this._context.TeamService.SaveTeamAsync(team).ConfigureAwait(false);

            return RedirectToAction("GetTeam", new { clubId = clubId, teamId = teamId });
        }

        ///// <summary>
        ///// Adds club details.
        ///// </summary>
        ///// <returns>Returns the club details input form.</returns>
        //[Route("add")]
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddCompetition(ClubAddViewModel model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest();
        //    }

        //    var club = this._context.Map<ClubAddViewModelToClubModelMapper, ClubModel>(model);

        //    var clubId = await this._context.ClubService.SaveClubAsync(club).ConfigureAwait(false);

        //    return RedirectToAction("GetClub", new { clubId = clubId });
        //}
    }
}