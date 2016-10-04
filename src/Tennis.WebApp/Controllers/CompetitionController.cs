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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for competitions.
    /// </summary>
    [Route("competitions")]
    public class CompetitionController : Controller
    {
        private readonly ICompetitionServiceContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionController"/> class.
        /// </summary>
        /// <param name="context"><see cref="ICompetitionServiceContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public CompetitionController(ICompetitionServiceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the list of competitions.
        /// </summary>
        /// <returns>Returns the list of competitions.</returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetCompetitions()
        {
            var competitions = await this._context.CompetitionService.GetCompetitionsAsync().ConfigureAwait(false);

            var vm = new CompetitionCollectionViewModel() { Competitions = competitions };

            return View("Index", vm);
        }

        /// <summary>
        /// Gets the competition details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the competition details.</returns>
        [Route("{competitionId}")]
        [HttpGet]
        public async Task<IActionResult> GetCompetition(Guid competitionId)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            var competition = await this._context.CompetitionService
                                        .GetCompetitionAsync(competitionId)
                                        .ConfigureAwait(false);

            var items = await this._context.ClubService
                                  .GetClubsAsync()
                                  .ConfigureAwait(false);

            var clubs = this._context.Map<ClubModelToSelectListItemMapper, List<SelectListItem>>(items);
            clubs.Insert(0, new SelectListItem() { Text = "Select Club", Value = string.Empty, Selected = true });

            var vm = new CompetitionViewModel() { Competition = competition, Clubs = clubs };

            return View("GetCompetition", vm);
        }

        /// <summary>
        /// Adds competition details.
        /// </summary>
        /// <returns>Returns the competition details input form.</returns>
        [Route("add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddCompetition()
        {
            var items = await this._context.DistrictService.GetDistrictsAsync().ConfigureAwait(false);
            var districts = this._context.Map<DistrictModelToSelectListItemMapper, List<SelectListItem>>(items);
            districts.Insert(0, new SelectListItem() { Text = "Select District", Selected = true });

            var year = DateTimeOffset.Now.Year;
            var seasons = new List<string> { "Spring", "Summer", "Autumn", "Winter" };
            var days = ((int[]) Enum.GetValues(typeof(DayOfWeek))).OrderBy(p => p).Select(p => Enum.GetName(typeof(DayOfWeek), p)).ToList();
            var types = new List<string> { "Boys", "Girls", "Open" };

            var vm = new CompetitionAddViewModel()
                     {
                         Districts = districts,
                         Year = year,
                         Seasons = seasons,
                         Days = days,
                         Types = types
                     };

            return View("AddCompetition", vm);
        }

        /// <summary>
        /// Adds competition details.
        /// </summary>
        /// <returns>Returns the competition details input form.</returns>
        [Route("add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompetition(CompetitionAddViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var competition = this._context.Map<CompetitionAddViewModelToCompetitionModelMapper, CompetitionModel>(model);
            var competitionId = await this._context.CompetitionService.SaveCompetitionAsync(competition).ConfigureAwait(false);

            return RedirectToAction("GetCompetition", new { competitionId = competitionId });
        }

        /// <summary>
        /// Adds a team to competition.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="model"><see cref="CompetitionViewModel"/> instance.</param>
        /// <returns>Returns the competition details.</returns>
        [Route("{competitionId}/teams/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam(Guid competitionId, CompetitionViewModel model)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var team = this._context.Map<CompetitionViewModelToTeamModelMapper, TeamModel>(model);
            team.ClubId = model.Club;
            team.CompetitionId = competitionId;

            await this._context.TeamService.SaveTeamAsync(team).ConfigureAwait(false);

            return RedirectToAction("GetCompetition", new { competitionId = competitionId });
        }

        /// <summary>
        /// Gets the fixture details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <returns>Returns the fixture details.</returns>
        [Route("{competitionId}/fixtures/{fixtureId}")]
        [HttpGet]
        public async Task<IActionResult> GetFixture(Guid competitionId, Guid fixtureId)
        {
            var fixture = await this._context.FixtureService.GetFixtureAsync(fixtureId).ConfigureAwait(false);

            var vm = new FixtureViewModel() { Fixture = fixture };

            return View("GetFixture", vm);
        }

        /// <summary>
        /// Adds fixture details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the fixture details input form.</returns>
        [Route("{competitionId}/fixtures/add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddFixture(Guid competitionId)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            var competition = await this._context.CompetitionService.GetCompetitionAsync(competitionId).ConfigureAwait(false);

            var items = await this._context.ClubService.GetClubsAsync().ConfigureAwait(false);
            var clubs = this._context.Map<ClubModelToSelectListItemMapper, List<SelectListItem>>(items);
            clubs.Insert(0, new SelectListItem() { Text = "Select Venue", Selected = true });

            var vm = new FixtureAddViewModel() { CompetitionId = competitionId, CompetitionName = competition.Name, Clubs = clubs };

            return View("AddFixture", vm);
        }

        /// <summary>
        /// Adds fixture details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="model"><see cref="FixtureAddViewModel"/> instance.</param>
        /// <returns>Returns the competition details page.</returns>
        [Route("{competitionId}/fixtures/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFixture(Guid competitionId, FixtureAddViewModel model)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var fixture = this._context.Map<FixtureAddViewModelToFixtureModelMapper, FixtureModel>(model);
            fixture.CompetitionId = competitionId;

            await this._context.FixtureService.SaveFixtureAsync(fixture).ConfigureAwait(false);

            return RedirectToAction("GetCompetition", new { competitionId = competitionId });
        }

        /// <summary>
        /// Gets the list of players belong to the team.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="teamId">Team Id.</param>
        /// <returns>Returns the list of players belong to the team.</returns>
        [Route("{competitionId}/teams/{teamId}/players")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPlayers(Guid competitionId, Guid teamId)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            if (teamId == Guid.Empty)
            {
                return NotFound();
            }

            var players = await this._context.PlayerService.GetPlayersByTeamIdAsync(teamId).ConfigureAwait(false);

            return new JsonResult(players.Select(p => new { name = $"{p.FirstName} {p.LastName}", playerId = p.PlayerId }));
        }

        /// <summary>
        /// Adds matches details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <returns></returns>
        [Route("{competitionId}/fixtures/{fixtureId}/matches/add")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddMatches(Guid competitionId, Guid fixtureId)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            if (fixtureId == Guid.Empty)
            {
                return NotFound();
            }

            var items = await this._context.TeamService.GetTeamsByCompetitionIdAsync(competitionId).ConfigureAwait(false);
            var teams = this._context.Map<TeamModelToSelectListItemMapper, List<SelectListItem>>(items);
            teams.Insert(0, new SelectListItem() { Text = "Select Team", Value = string.Empty, Selected = true });

            var vm = new MatchesAddViewModel() { CompetitionId = competitionId, FixtureId = fixtureId, Teams = teams, NumberOfPlayers = 3 };

            return View("AddMatches", vm);
        }

        /// <summary>
        /// Adds matches details.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <param name="model"><see cref="MatchesAddViewModel"/> instance.</param>
        /// <returns></returns>
        [Route("{competitionId}/fixtures/{fixtureId}/matches/add")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatches(Guid competitionId, Guid fixtureId, MatchesAddViewModel model)
        {
            if (competitionId == Guid.Empty)
            {
                return NotFound();
            }

            if (fixtureId == Guid.Empty)
            {
                return NotFound();
            }

            if (model == null)
            {
                return BadRequest();
            }

            await this._context.PlayerService.SaveMatchesAsync(fixtureId, model.HomePlayers, model.AwayPlayers).ConfigureAwait(false);

            return RedirectToAction("GetFixture", new { competitionId = competitionId, fixtureId = fixtureId });
        }
    }
}
