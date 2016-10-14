using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="MatchResultsUpdateViewModel"/> and <see cref="FixtureModel"/>.
    /// </summary>
    public class MatchResultsUpdateViewModelToFixtureModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<MatchResultsUpdateViewModel, FixtureModel>()
                  .ForMember(d => d.Club, o => o.Ignore())
                  .ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.HomeTeam, o => o.Ignore())
                  .ForMember(d => d.AwayTeam, o => o.Ignore())
                  .ForMember(d => d.Matches, o => o.MapFrom(s => GetMatches(s)))
                  ;
        }

        private static List<MatchModel> GetMatches(MatchResultsUpdateViewModel model)
        {
            var matches = model.MatchIds
                               .Select((matchId, i) => new MatchModel()
                                                       {
                                                           MatchId = matchId,
                                                           FixtureId = model.FixtureId,
                                                           SetNumber = model.SetNumbers[i],
                                                           HomeSetScore = model.HomeSetScores[i],
                                                           HomeGameScore = model.HomeGameScores[i],
                                                           AwayGameScore = model.AwayGameScores[i],
                                                           AwaySetScore = model.AwaySetScores[i]
                                                       })
                               .ToList();
            return matches;
        }
    }
}