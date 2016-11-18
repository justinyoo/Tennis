using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Tennis.ViewModels.Tournaments;
using Tennis.WebApp.ServiceContexts;

using Tournaments.Models;

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for players.
    /// </summary>
    [Route("players")]
    public class PlayerController : Controller
    {
        private readonly ITournamentServiceContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerController"/> class.
        /// </summary>
        /// <param name="context"><see cref="ITournamentServiceContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public PlayerController(ITournamentServiceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        /// <returns>Returns the list of players.</returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await this._context.PlayerService.GetPlayersAsync().ConfigureAwait(false);
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
            if (playerId == Guid.Empty)
            {
                return NotFound();
            }

            var player = await this._context.PlayerService.GetPlayerAsync(playerId).ConfigureAwait(false);
            var vm = new PlayerViewModel() { Player = player };

            return View("GetPlayer", vm);
        }

        ///// <summary>
        ///// Gets the tournament feeds for the given player.
        ///// </summary>
        ///// <param name="memberId">Member Id at tennis.com.au.</param>
        ///// <returns>Returns the tournament feeds for the given player.</returns>
        //[Route("{memberId}/feed")]
        //[HttpGet]
        //public async Task<IActionResult> GetTournamentsFeed(long memberId)
        //{
        //    var tournaments = await this._service.GetTournamentsFromFeedAsync(memberId).ConfigureAwait(false);

        //    return View(tournaments);
        //}

        /// <summary>
        /// Adds a new player.
        /// </summary>
        /// <param name="model"><see cref="PlayerCollectionViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="PlayerViewModel"/> instance.</returns>
        [Route("add")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPlayer(PlayerCollectionViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var memberId = await this._context.FeedService.GetMemberIdFromFeedUrlAsync(model.TournamentFeedUrl).ConfigureAwait(false);
            var feed = await this._context.FeedService.GetTournamentFeedByMemberIdAsync(memberId).ConfigureAwait(false);
            var names = await this._context.PlayerService.GetPlayerNameFromFeedAsync(feed.Title).ConfigureAwait(false);

            var playerId = await this._context.PlayerService.SavePlayerAsync(memberId, names).ConfigureAwait(false);

            foreach (var item in feed.Items)
            {
                var ptfim = new PlayerTournamentFeedItemModel() {PlayerId = playerId, Item = item };
                var tournamentId = await this._context.TournamentService.SaveTournamentAsync(ptfim).ConfigureAwait(false);
                var ptId = await this._context.PlayerService.SavePlayerTournamentAsync(tournamentId, ptfim).ConfigureAwait(false);
            }

            return RedirectToAction("GetPlayer", new { playerId = playerId });
        }
    }
}
