using System;

using AutoMapper;

using Tennis.Mappers;
using Tennis.ViewModels.Tournaments;

using Tournaments.Helpers;
using Tournaments.Models;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="TournamentAddViewModel"/> and <see cref="TournamentModel"/>.
    /// </summary>
    public class TournamentAddViewModelToTournamentModelMapper : BaseMapper
    {
        private readonly IFeedHelper _helper;

        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentAddViewModelToTournamentModelMapper"/> class.
        /// </summary>
        public TournamentAddViewModelToTournamentModelMapper()
        {
            this._helper = new FeedHelper();
        }

        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<TournamentAddViewModel, TournamentModel>()
                  .ForMember(d => d.TournamentKey, o => o.MapFrom(s => GetTournamentKey(s)))
                  .ForMember(d => d.PlayerNumber, o => o.MapFrom(s => GetPlayerNumber(s)))
                  .ForMember(d => d.Summary, o => o.MapFrom(s => GetSummary(s)))
                  ;
        }

        private Guid GetTournamentKey(TournamentAddViewModel model)
        {
            var key = this._helper.GetTournamentKeyFromUrl(model.TournamentPlayerUrl);

            return key;
        }

        private int GetPlayerNumber(TournamentAddViewModel model)
        {
            var number = this._helper.GetPlayerNumberFromUrl(model.TournamentPlayerUrl);

            return number;
        }

        private static string GetSummary(TournamentAddViewModel model)
        {
            return model.IsSummarySameAsTitle ? model.Title : model.Summary;
        }
    }
}