using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Tennis.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for competitions.
    /// </summary>
    [Authorize]
    [Route("competitions")]
    public class CompetitionController : Controller
    {
        /// <summary>
        /// Gets the list of competitions.
        /// </summary>
        /// <returns>Returns the list of competitions.</returns>
        [Route("")]
        public IActionResult GetCompetitions()
        {
            var competitions = new List<CompetitionModel>()
                               {
                                   new CompetitionModel() { Name = "Waverley District Summer Competition" }
                               };
            var vm = new CompetitionCollectionViewModel() { Competions = competitions };

            return View("Index", vm);
        }
    }
}
