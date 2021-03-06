﻿using System.Linq;
using System.ServiceModel.Syndication;

using AutoMapper;

using Tennis.Mappers;

using Tournaments.Models;

namespace Tournaments.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="SyndicationFeed"/> and <see cref="TournamentFeedModel"/>.
    /// </summary>
    public class FeedToTournamentFeedModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<SyndicationFeed, TournamentFeedModel>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title.Text))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description.Text))
                .ForMember(d => d.LinkUrl, o => o.MapFrom(s => s.Links.First().Uri.ToString()))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items.ToList()))
                ;

            config.CreateMap<SyndicationItem, TournamentFeedItemModel>()
                .ForMember(d => d.ItemId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title.Text))
                .ForMember(d => d.Summary, o => o.MapFrom(s => s.Summary.Text))
                .ForMember(d => d.LinkUrl, o => o.MapFrom(s => s.Links.First().Uri.ToString()))
                .ForMember(d => d.DatePublished, o => o.MapFrom(s => s.PublishDate))
                ;
        }
    }
}