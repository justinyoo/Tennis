using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="FixtureAddViewModel"/> and <see cref="FixtureModel"/>.
    /// </summary>
    public class FixtureAddViewModelToFixtureModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<FixtureAddViewModel, FixtureModel>()
                  .ForMember(d => d.ClubId, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.Club, o => o.Ignore())
                  .ForMember(d => d.Week, o => o.MapFrom(s => s.Round.GetValueOrDefault()))
                  .ForMember(d => d.HomeTeamId, o => o.MapFrom(s => s.HomeTeam))
                  .ForMember(d => d.AwayTeamId, o => o.MapFrom(s => s.AwayTeam))
                  .ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.HomeTeam, o => o.Ignore())
                  .ForMember(d => d.AwayTeam, o => o.Ignore())
                  .ForMember(d => d.Matches, o => o.Ignore())
                  ;
        }
    }
}