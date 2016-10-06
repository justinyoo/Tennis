using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="TeamViewModel"/> and <see cref="TeamModel"/>.
    /// </summary>
    public class TeamViewModelToTeamModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<TeamViewModel, TeamModel>()
                  //.ForMember(d => d.CompetitionId, o => o.MapFrom(s => s.Competition))
                  //.ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.Club, o => o.Ignore())
                  .ForMember(d => d.TeamPlayers, o => o.Ignore())
                ;
        }
    }
}