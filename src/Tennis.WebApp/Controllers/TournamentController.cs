using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Tennis.ViewModels.Tournaments;
using Tennis.WebApp.Mappers;
using Tennis.WebApp.ServiceContexts;

using Tournaments.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for tournaments.
    /// </summary>
    [Route("tournaments")]
    public class TournamentController : Controller
    {
        private readonly ITournamentServiceContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentController"/> class.
        /// </summary>
        /// <param name="context"><see cref="ITournamentServiceContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public TournamentController(ITournamentServiceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Adds tournament details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <returns>Returns the tournament details input form.</returns>
        [Route("add")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddTournament([FromQuery] Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                return NotFound();
            }

            var player = await this._context.PlayerService.GetPlayerAsync(playerId).ConfigureAwait(false);

            var vm = new TournamentAddViewModel() { Player = player };

            return View("AddTournament", vm);
        }

        /// <summary>
        /// Adds the tournament details.
        /// </summary>
        /// <param name="playerId">Player Id.</param>
        /// <param name="model"><see cref="TournamentAddViewModel"/> instance.</param>
        /// <returns>Returns the player details page.</returns>
        [Route("add")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTournament([FromQuery] Guid playerId, TournamentAddViewModel model)
        {
            if (playerId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var tournament = this._context.Map<TournamentAddViewModelToTournamentModelMapper, TournamentModel>(model);
            var tournamentId = await this._context.TournamentService.SaveTournamentAsync(tournament).ConfigureAwait(false);
            var playerTournamentId = await this._context.PlayerService.SavePlayerTournamentAsync(playerId, tournamentId, tournament.PlayerNumber).ConfigureAwait(false);

            return RedirectToAction("GetPlayer", "Player", new { playerId = playerId });
        }
    }
}
