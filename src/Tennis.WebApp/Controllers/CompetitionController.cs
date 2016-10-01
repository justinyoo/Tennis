using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Tennis.ViewModels;
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

            var vm = new CompetitionViewModel() { Competition = competition };

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
            var types = new List<string> { "Boys", "Girls", "Open" };

            var vm = new CompetitionAddViewModel()
                     {
                         Districts = districts,
                         Year = year,
                         Seasons = seasons,
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

            var competition= this._context.Map<CompetitionAddViewModelToCompetitionModelMapper, CompetitionModel>(model);
            var competitionId = await this._context.CompetitionService.SaveCompetitionAsync(competition).ConfigureAwait(false);

            return RedirectToAction("GetCompetition", new { competitionId = competitionId });
        }
    }
}
