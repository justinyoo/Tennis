using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="CompetitionAddViewModel"/> and <see cref="CompetitionModel"/>.
    /// </summary>
    public class CompetitionAddViewModelToCompetitionModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<CompetitionAddViewModel, CompetitionModel>()
                  .ForMember(d => d.DistrictId, o => o.MapFrom(s => s.District))
                  ;
        }
    }
}