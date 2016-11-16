using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.Models;

namespace Tennis.ViewModels.Tournaments
{
    public class TournamentViewModel
    {
        public TournamentViewModel()
        {
            this.IsSummarySameAsTitle = true;
        }

        public PlayerModel Player { get; set; }

        [Display(Name = "Tournament Player URL")]
        public string TournamentPlayerUrl { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool IsSummarySameAsTitle { get; set; }

        [Display(Name = "Date Published")]
        public DateTimeOffset DatePublished { get; set; }
    }
}
