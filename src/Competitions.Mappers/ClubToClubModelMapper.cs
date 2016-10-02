﻿using System.Collections.Generic;
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
                  .ForMember(d => d.Players, o => o.MapFrom(s => s.Players))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  ;

            config.CreateMap<Player, PlayerModel>()
                  .ForMember(d => d.Club, o => o.Ignore())
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;
        }

        /// <summary>
        /// Gets the list of contacts from the club details.
        /// </summary>
        /// <param name="club"><see cref="Club"/> instance.</param>
        /// <returns>Returns the list of contacts.</returns>
        public static List<string> GetContacts(Club club)
        {
            var contacts = new List<string>() { club.Phone.ToPhone(), club.Mobile.ToMobile(), club.Email }.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            return contacts;
        }
    }
}