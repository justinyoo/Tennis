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
                  .ForMember(d => d.ClubHousePhone, o => o.MapFrom(s => s.ClubHousePhone.ToPhone()))
                  .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone.ToPhone()))
                  .ForMember(d => d.Mobile, o => o.MapFrom(s => s.Mobile.ToMobile()))
                  .ForMember(d => d.Contacts, o => o.MapFrom(s => GetContacts(s)))
                  .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue))
                  .ForMember(d => d.ClubPlayers, o => o.MapFrom(s => s.ClubPlayers))
                  .ForMember(d => d.Teams, o => o.MapFrom(s => s.Teams))
                  ;

            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => VenueToVenueModelMapper.GetFullAddress(s)))
                  ;

            config.CreateMap<ClubPlayer, ClubPlayerModel>()
                  .ForMember(d => d.Club, o => o.Ignore())
                  .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
                  ;

            config.CreateMap<Team, TeamModel>()
                  .ForMember(d => d.Club, o => o.Ignore())
                  .ForMember(d => d.CompetitionTeams, o => o.Ignore())
                  .ForMember(d => d.TeamPlayers, o => o.MapFrom(s => s.TeamPlayers))
                  ;

            config.CreateMap<TeamPlayer, TeamPlayerModel>()
                  .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
                  ;

            config.CreateMap<Player, PlayerModel>()
                  .ForMember(d => d.TeamPlayers, o => o.Ignore())
                  ;
        }

        /// <summary>
        /// Gets the list of contacts from the club details.
        /// </summary>
        /// <param name="club"><see cref="Club"/> instance.</param>
        /// <returns>Returns the list of contacts.</returns>
        public static List<string> GetContacts(Club club)
        {
            var contacts = new List<string>() { club.ClubHousePhone.ToPhone(), club.Phone.ToPhone(), club.Mobile.ToMobile(), club.Email }.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            return contacts;
        }
    }
}