using AutoMapper;

using Tennis.Mappers;

using Tournaments.EntityModels;
using Tournaments.Models;

namespace Tournaments.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Tournament"/> and <see cref="TournamentModel"/>.
    /// </summary>
    public class TournamentToTournamentModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Tournament, TournamentModel>()
                  .ForMember(d => d.PlayerNumber, o => o.Ignore())
                ;
        }
    }
}