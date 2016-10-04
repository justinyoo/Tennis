using AutoMapper;

using Competitions.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using Tennis.Mappers;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="ClubModel"/> and <see cref="SelectListItem"/>.
    /// </summary>
    public class TeamModelToSelectListItemMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<TeamModel, SelectListItem>()
                  .ForMember(d => d.Text, o => o.MapFrom(s => $"{s.Club.Name} {s.Name} {s.Tag}"))
                  .ForMember(d => d.Value, o => o.MapFrom(s => s.TeamId))
                  ;
        }
    }
}