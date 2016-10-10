using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="CompetitionViewModel"/> and <see cref="CompetitionTeamModel"/>.
    /// </summary>
    public class CompetitionViewModelToCompetitionTeamModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<CompetitionViewModel, CompetitionTeamModel>()
                  .ForMember(d => d.TeamId, o => o.MapFrom(s => s.Team))
                  .ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.Team, o => o.Ignore())
                ;
        }
    }
}