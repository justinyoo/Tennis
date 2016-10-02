using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="ClubAddViewModel"/> and <see cref="ClubModel"/>.
    /// </summary>
    public class ClubAddViewModelToClubModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<ClubAddViewModel, ClubModel>()
                ;
        }
    }
}