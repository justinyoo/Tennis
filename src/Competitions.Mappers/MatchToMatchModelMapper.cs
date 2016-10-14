using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Match"/> and <see cref="MatchModel"/>.
    /// </summary>
    public class MatchToMatchModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Match, MatchModel>()
                  .ForMember(d => d.MatchPlayers, o => o.MapFrom(s => s.MatchPlayers))
                  ;

            config.CreateMap<MatchPlayer, MatchPlayerModel>()
                  .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
                  ;

            config.CreateMap<Player, PlayerModel>()
                  .ForMember(d => d.TeamPlayers, o => o.Ignore())
                  ;
        }
    }
}