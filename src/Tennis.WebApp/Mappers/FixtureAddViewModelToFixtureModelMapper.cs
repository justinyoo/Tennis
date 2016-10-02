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
                  .ForMember(d => d.VenueId, o => o.MapFrom(s => s.Venue))
                  .ForMember(d => d.Venue, o => o.Ignore())
                  .ForMember(d => d.Week, o => o.MapFrom(s => s.Week.GetValueOrDefault()))
                ;
        }
    }
}