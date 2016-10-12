using System.IO;

using AutoMapper;

using Microsoft.AspNetCore.Http;

using Tennis.Common.Blob;
using Tennis.Mappers;
using Tennis.ViewModels.Competitions;

namespace Tennis.WebApp.Mappers
{
    /// <summary>
    /// This represents the mapper entity between <see cref="FixtureViewModel"/> and <see cref="BlobUploadRequest"/>.
    /// </summary>
    public class FixtureViewModelToBlobUploadRequestMapper : BaseMapper
    {
        /// <summary>
        /// Configures the mapping information between source and destination.
        /// </summary>
        /// <param name="config"><see cref="IMapperConfigurationExpression"/> instance.</param>
        protected override void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<FixtureViewModel, BlobUploadRequest>()
                  .ForMember(d => d.BlobName, o => o.MapFrom(s => $"fixtures/{s.FixtureId}/{s.ScoreSheet.FileName}"))
                  .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ScoreSheet.ContentType))
                  .ForMember(d => d.Stream, o => o.MapFrom(s => CloneStream(s.ScoreSheet)))
                ;
        }

        private static Stream CloneStream(IFormFile file)
        {
            var stream = file.OpenReadStream();
            return stream;
        }
    }
}