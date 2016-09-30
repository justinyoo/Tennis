using AutoMapper;

using Tennis.Mappers;

using Tournaments.EntityModels;
using Tournaments.Models;

namespace Tournaments.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="PlayerTournament"/> and <see cref="TournamentModel"/>.
    /// </summary>
    public class PlayerTournamentToTournamentModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<PlayerTournament, TournamentModel>()
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.Tournament.TournamentId))
                .ForMember(d => d.TournamentKey, o => o.MapFrom(s => s.Tournament.TournamentKey))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Tournament.Title))
                .ForMember(d => d.Summary, o => o.MapFrom(s => s.Tournament.Summary))
                .ForMember(d => d.DatePublished, o => o.MapFrom(s => s.Tournament.DatePublished))
                .ForMember(d => d.PlayerNumber, o => o.MapFrom(s => s.PlayerNumber))
                ;
        }
    }
}