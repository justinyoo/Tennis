using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="District"/> and <see cref="DistrictModel"/>.
    /// </summary>
    public class DistrictToDistrictModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<District, DistrictModel>()
                ;
        }
    }
}