using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="FixtureModel"/> and <see cref="FixtureJsonViewModel"/>.
    /// </summary>
    public class FixtureModelToFixtureJsonViewModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<FixtureModel, FixtureJsonViewModel>()
                  .ForMember(d => d.FixtureId, o => o.MapFrom(s => s.FixtureId))
                  .ForMember(d => d.CompetitionId, o => o.MapFrom(s => s.CompetitionId))
                  .ForMember(d => d.Round, o => o.MapFrom(s => s.Week))
                  .ForMember(d => d.DateScheduled, o => o.MapFrom(s => s.DateScheduled))
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club.Name))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Club.Venue.FullAddress))
                ;
        }
    }
}