using System.Linq;

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
                  .ForMember(d => d.ParticipatingClubs, o => o.MapFrom(s => s.CompetitionClubs.Select(q => q.Club).ToList()))
                  ;

            config.CreateMap<CompetitionClub, ClubModel>()
                  .ForMember(d => d.Tag, o => o.MapFrom(s => s.ClubTag))
                  ;

            config.CreateMap<Club, ClubModel>()
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => ClubToClubModelMapper.GetContacts(s)))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;
        }
    }
}