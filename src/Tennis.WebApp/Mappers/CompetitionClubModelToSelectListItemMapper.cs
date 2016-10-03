using AutoMapper;

using Competitions.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using Tennis.Mappers;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="CompetitionClubModel"/> and <see cref="SelectListItem"/>.
    /// </summary>
    public class CompetitionClubModelToSelectListItemMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            //config.CreateMap<CompetitionClubModel, SelectListItem>()
            //      .ForMember(d => d.Text, o => o.MapFrom(s => $"{s.Club.Name} - {s.Club.Manager}"))
            //      .ForMember(d => d.Value, o => o.MapFrom(s => s.ClubId))
            //    ;
        }
    }
}