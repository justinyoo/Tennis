﻿using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TournamentHistory.Models;
using TournamentHistory.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for players.
    /// </summary>
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

            return View(players);
        }

        /// <summary>
        /// Gets the player details.
        /// </summary>
        /// <param name="memberId">Member Id at tennis.com.au.</param>
        /// <returns>Returns the player details.</returns>
        [Route("{memberId}")]
        [HttpGet]
        public async Task<IActionResult> GetPlayer(long memberId)
        {
            var player = await this._service.GetPlayerAsync(memberId).ConfigureAwait(false);

            return View(player);
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

        [Route("add")]
        [HttpGet]
        public async Task<IActionResult> AddPlayer()
        {
            var model = new PlayerModel();

            return View(model);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddPlayer(PlayerModel model)
        {
            return View(model);
        }
    }
}
