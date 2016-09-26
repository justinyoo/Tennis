using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TournamentHistory.Services;
using TournamentHistory.ViewModels;

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for players.
    /// </summary>
    [Authorize]
    [Route("players")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _service;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerController"/> class.
        /// </summary>
        /// <param name="service"><see cref="IPlayerService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="service"/> is <see langword="null" />.</exception>
        public PlayerController(IPlayerService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        /// <returns>Returns the list of players.</returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await this._service.GetPlayersAsync().ConfigureAwait(false);
            var vm = new PlayerCollectionViewModel() { Players = players };

            return View("Index", vm);
        }

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the player details.</returns>
        [Route("{playerId}")]
        [HttpGet]
        public async Task<IActionResult> GetPlayer(Guid playerId)
        {
            var player = await this._service.GetPlayerAsync(playerId).ConfigureAwait(false);
            var vm = new PlayerViewModel() { Player = player };

            return View("GetPlayer", vm);
        }

        /// <summary>
        /// Gets the tournament feeds for the given player.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the tournament feeds for the given player.</returns>
        [Route("{memberId}/feed")]
        [HttpGet]
        public async Task<IActionResult> GetTournamentsFeed(long memberId)
        {
            var tournaments = await this._service.GetTournamentsFromFeedAsync(memberId).ConfigureAwait(false);

            return View(tournaments);
        }

        /// <summary>
        /// Adds a new player.
        /// </summary>
        /// <param name="model"><see cref="PlayerCollectionViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="PlayerViewModel"/> instance.</returns>
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddPlayer(PlayerCollectionViewModel model)
        {
            var player = await this._service.SaveTournamentsFromFeedAsync(model.TournamentFeedUrl).ConfigureAwait(false);
            var vm = new PlayerViewModel() { Player = player };

            return View("AddPlayer", vm);
        }
    }
}
