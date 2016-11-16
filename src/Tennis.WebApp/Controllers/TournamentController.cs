using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tennis.ViewModels.Tournaments;
using Tournaments.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tennis.WebApp.Controllers
{
    [Route("tournaments")]
    public class TournamentController : Controller
    {
        private readonly IPlayerService _service;
        
        public TournamentController(IPlayerService service)
        {
            this._service = service;
        }

        [Route("add")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddTournament([FromQuery] Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                return NotFound();
            }

            var player = await this._service.GetPlayerAsync(playerId).ConfigureAwait(false);

            var vm = new TournamentViewModel() { Player = player };

            return View("AddTournament", vm);
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTournament([FromQuery] Guid playerId, TournamentViewModel model)
        {
            if (playerId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var vm = model;

            return View("AddTournament", vm);
        }
    }
}
