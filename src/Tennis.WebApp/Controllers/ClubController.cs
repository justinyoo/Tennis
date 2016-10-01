using System;
using System.Threading.Tasks;

using Competitions.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Tennis.ViewModels;
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
            var venue = await this._context.VenueService.GetVenueAsync(club.VenueId).ConfigureAwait(false);

            var vm = new ClubViewModel() { Club = club, Venue = venue };

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
    }
}