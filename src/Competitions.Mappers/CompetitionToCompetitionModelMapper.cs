using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Common.Extensions;
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
                  .ForMember(d => d.TrolsFixture, o => o.MapFrom(s => s.District.TrolsFixture))
                  .ForMember(d => d.TrolsResults, o => o.MapFrom(s => s.District.TrolsResults))
                  .ForMember(d => d.TrolsLadders, o => o.MapFrom(s => s.District.TrolsLadders))
                  .ForMember(d => d.CompetitionTeams, o => o.MapFrom(s => s.CompetitionTeams))
                  .ForMember(d => d.Fixtures, o => o.MapFrom(s => s.Fixtures))
                  ;

            config.CreateMap<CompetitionTeam, CompetitionTeamModel>()
                  .ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.Team, o => o.MapFrom(s => s.Team))
                  ;

            config.CreateMap<Team, TeamModel>()
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.CompetitionTeams, o => o.Ignore())
                  .ForMember(d => d.TeamPlayers, o => o.Ignore())
                  ;

            config.CreateMap<Fixture, FixtureModel>()
                  .ForMember(d => d.Club, o => o.MapFrom(s => s.Club))
                  .ForMember(d => d.Competition, o => o.Ignore())
                  .ForMember(d => d.HomeTeam, o => o.Ignore())
                  .ForMember(d => d.AwayTeam, o => o.Ignore())
                  .ForMember(d => d.Matches, o => o.Ignore())
                  ;

            config.CreateMap<Club, ClubModel>()
                  .ForMember(d => d.ClubHousePhone, o => o.MapFrom(s => s.ClubHousePhone.ToPhone()))
                  .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone.ToPhone()))
                  .ForMember(d => d.Mobile, o => o.MapFrom(s => s.Mobile.ToMobile()))
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => ClubToClubModelMapper.GetContacts(s)))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  .ForMember(d => d.ClubPlayers, o => o.Ignore())
                  .ForMember(d => d.Teams, o => o.Ignore())
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;
        }
    }
}