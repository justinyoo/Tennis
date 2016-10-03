using System;
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
        /// <returns>Returns the club details input form.</returns>
        [Route("add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompetition(ClubAddViewModel model)
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

            throw new NotImplementedException();
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
            
            var vm = new TeamViewModel() { Team = team };

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

            var vm = new TeamAddViewModel() { ClubId = clubId, ClubName = club.Name };

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

        ///// <summary>
        ///// Gets the list of players.
        ///// </summary>
        ///// <param name="clubId">Club Id.</param>
        ///// <returns>Returns the list of players.</returns>
        //[Route("{clubId}/players")]
        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetPlayers(Guid clubId)
        //{
        //    if (clubId == Guid.Empty)
        //    {
        //        return NotFound();
        //    }

        //    var players = await this._context.PlayerService.GetPlayersAsync(clubId).ConfigureAwait(false);
        //    var sorted = players.OrderBy(p => p.FirstName)
        //                        .ThenBy(p => p.LastName)
        //                        .Select(p => new { PlayerId = p.PlayerId, Name = $"{p.FirstName} {p.LastName}" });
        //    return new JsonResult(sorted);
        //}

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the player input form.</returns>
        [Route("{clubId}/players/add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddPlayer(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            var club = await this._context.ClubService.GetClubAsync(clubId).ConfigureAwait(false);

            var vm = new PlayerAddViewModel() { ClubId = clubId, ClubName = club.Name };

            return View("AddPlayer", vm);
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <param name="model"><see cref="PlayerAddViewModel"/> instance.</param>
        /// <returns>Returns the player input form.</returns>
        [Route("{clubId}/players/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlayer(Guid clubId, PlayerAddViewModel model)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var player = this._context.Map<PlayerAddViewModelToPlayerModelMapper, PlayerModel>(model);
            //player.ClubId = clubId;

            await this._context.PlayerService.SavePlayerAsync(player).ConfigureAwait(false);

            return RedirectToAction("GetClub", new { clubId = clubId });
        }
    }
}