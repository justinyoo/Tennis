using AutoMapper;

using Tennis.Mappers;

using Tournaments.EntityModels;
using Tournaments.Models;

namespace Tournaments.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="Player"/> and <see cref="PlayerModel"/>.
    /// </summary>
    public class PlayerToPlayerModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Player, PlayerModel>()
                  .ForMember(d => d.Name, o => o.MapFrom(s => GetPlayerName(s)))
                  .ForMember(d => d.Tournaments, o => o.Ignore())
                  ;
        }

        /// <summary>
        /// Gets the player name.
        /// </summary>
        /// <param name="player"><see cref="Player"/> instance.</param>
        /// <returns>Returns the player name.</returns>
        public static string GetPlayerName(Player player)
        {
            var name = $"{player.FirstName} {GetPlayerMiddleNames(player)} {player.LastName}";

            return name;
        }

        private static string GetPlayerMiddleNames(Player player)
        {
            var name = string.IsNullOrWhiteSpace(player.MiddleNames) ? string.Empty : player.MiddleNames;

            return name;
        }
    }
}