using System;
using System.Collections.Generic;

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
                  .ForMember(d => d.CompetitionTeams, o => o.MapFrom(s => GetCompetitionTeams(s)))
                  .ForMember(d => d.TeamPlayers, o => o.MapFrom(s => GetTeamPlayers(s)))
                  .ForMember(d => d.Club, o => o.Ignore())
                  ;
        }

        private static List<CompetitionTeamModel> GetCompetitionTeams(TeamViewModel model)
        {
            if (model.Competition == Guid.Empty)
            {
                return null;
            }

            var cts = new List<CompetitionTeamModel>()
                      {
                          new CompetitionTeamModel() { TeamId = model.TeamId, CompetitionId = model.Competition, TeamNumber = model.TeamNumber }
                      };
            return cts;
        }

        private static List<TeamPlayerModel> GetTeamPlayers(TeamViewModel model)
        {
            if (model.Player == Guid.Empty)
            {
                return null;
            }

            var tps = new List<TeamPlayerModel>()
                      {
                          new TeamPlayerModel() { TeamId = model.TeamId, PlayerId = model.Player, Order = model.PlayerOrder }
                      };
            return tps;
        }
    }
}