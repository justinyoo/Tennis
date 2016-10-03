using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Fixture"/> and <see cref="FixtureModel"/>.
    /// </summary>
    public class FixtureToFixtureModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Fixture, FixtureModel>()
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.Matches, o => o.MapFrom(s => s.Matches))
                  ;

            config.CreateMap<Club, ClubModel>()
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => ClubToClubModelMapper.GetContacts(s)))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;

            config.CreateMap<Match, MatchModel>()
                  .ForMember(d => d.MatchPlayers, o => o.MapFrom(s => s.MatchPlayers))
                  ;

            config.CreateMap<MatchPlayer, MatchPlayerModel>()
                  .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
                  ;

            config.CreateMap<Player, PlayerModel>()
                  ;
        }
    }
}