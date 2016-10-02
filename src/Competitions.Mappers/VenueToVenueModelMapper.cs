using System.Linq;

using AutoMapper;

using Competitions.EntityModels;
using Competitions.Models;

using Tennis.Mappers;

namespace Competitions.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Venue"/> and <see cref="VenueModel"/>.
    /// </summary>
    public class VenueToVenueModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Venue, VenueModel>()
                  .ForMember(d => d.FullAddress, o => o.MapFrom(s => GetFullAddress(s)))
                  ;
        }

        /// <summary>
        /// Gets the full address details from venue details.
        /// </summary>
        /// <param name="venue"><see cref="Venue"/> instance.</param>
        /// <returns>Retuns the full address details.</returns>
        public static string GetFullAddress(Venue venue)
        {
            var segments = new[] { venue.Address1, venue.Address2, venue.Suburb, venue.State, venue.Postcode }
                                 .Where(p => !string.IsNullOrWhiteSpace(p))
                                 .ToList();
            var address = string.Join(", ", segments);
            return address;
        }
    }
}
