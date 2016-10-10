using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Competitions.Models;

using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="PlayersAddViewModel"/> and <see cref="ClubPlayerCollectionModel"/>.
    /// </summary>
    public class PlayersAddViewModelToClubPlayerCollectionModelMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<PlayersAddViewModel, ClubPlayerCollectionModel>()
                  .ForMember(d => d.ClubPlayers, o => o.MapFrom(s => GetClubPlayers(s)))
                ;
        }

        private static List<ClubPlayerModel> GetClubPlayers(PlayersAddViewModel model)
        {
            var players = model.FirstNames
                               .Select(
                                       (fn, i) =>
                                           new ClubPlayerModel()
                                           {
                                               Player = new PlayerModel() { FirstName = fn, LastName = model.LastNames[i] },
                                           })
                               .ToList();

            return players;
        }
    }
}