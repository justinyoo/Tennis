﻿using System.Linq;

using AutoMapper;

using TournamentHistory.EntityModels;
using TournamentHistory.Models;

namespace TournamentHistory.Mappers
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
                ;
        }
    }
}