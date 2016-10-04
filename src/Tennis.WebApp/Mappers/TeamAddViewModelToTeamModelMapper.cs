using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="TeamAddViewModel"/> and <see cref="TeamModel"/>.
    /// </summary>
    public class TeamAddViewModelToTeamModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<TeamAddViewModel, TeamModel>()
                  .ForMember(d => d.TeamPlayers, o => o.MapFrom(s => GetTeamPlayers(s)))
                  ;
        }

        private static List<TeamPlayerModel> GetTeamPlayers(TeamAddViewModel model)
        {
            var tps = model.FirstNames
                           .Select(
                                   (fn, i) =>
                                       new TeamPlayerModel()
                                       {
                                           Order = i + 1,
                                           Player = new PlayerModel() { FirstName = fn, LastName = model.LastNames[i] }
                                       })
                           .ToList();

            return tps;
        }
    }
}