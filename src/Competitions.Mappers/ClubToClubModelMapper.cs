using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;
using Tennis.Common.Extensions;
using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Club"/> and <see cref="ClubModel"/>.
    /// </summary>
    public class ClubToClubModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Club, ClubModel>()
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => GetContacts(s)))
                  ;
        }

        private static List<string> GetContacts(Club club)
        {
            var contacts = new List<string>() { club.Phone.ToPhone(), club.Mobile.ToMobile(), club.Email }.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            return contacts;
        }
    }
}