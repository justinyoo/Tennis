using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="CompetitionViewModel"/> and <see cref="CompetitionClubModel"/>.
    /// </summary>
    public class CompetitionViewModelToCompetitionClubModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<CompetitionViewModel, CompetitionClubModel>()
                  .ForMember(d => d.ClubId, o => o.MapFrom(s => s.Club))
                ;
        }
    }
}