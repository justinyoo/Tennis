using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Competition"/> and <see cref="CompetitionModel"/>.
    /// </summary>
    public class CompetitionToCompetitionModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Competition, CompetitionModel>()
                  .ForMember(d => d.TrolsUrl, o => o.MapFrom(s => s.District.TrolsUrl))
                  .ForMember(d => d.Teams, o => o.MapFrom(s => s.Teams))
                  .ForMember(d => d.Fixtures, o => o.MapFrom(s => s.Fixtures))
                  ;

            config.CreateMap<Team, TeamModel>()
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.TeamPlayers, o => o.Ignore())
                  ;

            config.CreateMap<Fixture, FixtureModel>()
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.Matches, o => o.Ignore())
                  ;

            config.CreateMap<Club, ClubModel>()
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => ClubToClubModelMapper.GetContacts(s)))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  .ForMember(d => d.Teams, o => o.Ignore())
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;
        }
    }
}