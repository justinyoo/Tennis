using System;
using System.Linq;
using System.Threading.Tasks;

using Competitions.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var clubs = await this._context.ClubService.GetClubsAsync().ConfigureAwait(false);

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
            var club = await this._context.ClubService.GetClubAsync(clubId).ConfigureAwait(false);

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
        public IActionResult AddClub()
        {
            var vm = new ClubAddViewModel() { };

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
            var venue = this._context.Map<ClubAddViewModelToVenueModelMapper, VenueModel>(model);
            var clubId = await this._context.ClubService.SaveClubAsync(club, venue).ConfigureAwait(false);

            return RedirectToAction("GetClub", new { clubId = clubId });
        }

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        /// <param name="clubId">Club Id.</param>
        /// <returns>Returns the list of players.</returns>
        [Route("{clubId}/players")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPlayers(Guid clubId)
        {
            if (clubId == Guid.Empty)
            {
                return NotFound();
            }

            var players = await this._context.PlayerService.GetPlayersAsync(clubId).ConfigureAwait(false);
            var sorted = players.OrderBy(p => p.FirstName)
                                .ThenBy(p => p.LastName)
                                .Select(p => new { PlayerId = p.PlayerId, Name = $"{p.FirstName} {p.LastName}" });
            return new JsonResult(sorted);
        }

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
            player.ClubId = clubId;

            await this._context.PlayerService.SavePlayerAsync(player).ConfigureAwait(false);

            return RedirectToAction("GetClub", new { clubId = clubId });
        }
    }
}