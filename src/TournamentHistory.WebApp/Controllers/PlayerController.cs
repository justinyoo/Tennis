using System;

using Microsoft.AspNetCore.Mvc;

using TournamentHistory.Mappers;
using TournamentHistory.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TournamentHistory.WebApp.Controllers
{
    [Route("players")]
    public class PlayerController : Controller
    {
        private const string FeedUrl = "http://tournaments.tennis.com.au/feed/player.aspx?memberid={0}";

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetPlayerFeed(long id)
        {
            var mapper = new FeedToTournamentFeedModelMapper();
            using (var service = new PlayerService(mapper))
            {
                var items = service.GetTournamentsFromFeed(Convert.ToInt64(id));

                return new JsonResult(items);
            }
        }
    }
}