using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetPlayerFeed(long id)
        {
            using (var syndication = new SyndicationFeedWrapper())
            using (var mapper = new FeedToTournamentFeedModelMapper())
            using (var service = new PlayerService(syndication, mapper))
            {
                var items = await service.GetTournamentsFromFeedAsync(Convert.ToInt64(id)).ConfigureAwait(false);

                return new JsonResult(items);
            }
        }
    }
}