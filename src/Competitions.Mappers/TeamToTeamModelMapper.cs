using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Team"/> and <see cref="TeamModel"/>.
    /// </summary>
    public class TeamToTeamModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Team, TeamModel>()
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.Competition, o => o.Condition(s => s.Competition != null))
                  .ForMember(d => d.TeamPlayers, o => o.MapFrom(s => s.TeamPlayers))
                  ;

            config.CreateMap<Club, ClubModel>()
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => ClubToClubModelMapper.GetContacts(s)))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  .ForMember(d => d.Teams, o => o.Ignore())
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;

            config.CreateMap<Competition, CompetitionModel>()
                  .ForMember(d => d.Fixtures, o => o.Ignore())
                  .ForMember(d => d.Teams, o => o.Ignore())
                  ;

            config.CreateMap<TeamPlayer, TeamPlayerModel>()
                  .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
                  ;

            config.CreateMap<Player, PlayerModel>()
                  ;
        }
    }
}