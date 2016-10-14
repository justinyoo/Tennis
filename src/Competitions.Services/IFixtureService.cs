using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Competitions.Models;

using Tennis.Common.Blob;

namespace Competitions.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="FixtureService"/> class.
    /// </summary>
    public interface IFixtureService : IDisposable
    {
        /// <summary>
        /// Gets the list of fixtures.
        /// </summary>
        /// <param name="competitionId">Competition Id.</param>
        /// <returns>Returns the list of fixtures.</returns>
        Task<List<FixtureModel>> GetFixturesAsync(Guid competitionId);

        /// <summary>
        /// Gets the fixture details.
        /// </summary>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <returns>Returns the fixture details.</returns>
        Task<FixtureModel> GetFixtureAsync(Guid fixtureId);

        /// <summary>
        /// Saves the fixture details.
        /// </summary>
        /// <param name="model"><see cref="FixtureModel"/> instance.</param>
        /// <returns>Returns the fixture Id from the fixture details.</returns>
        Task<Guid> SaveFixtureAsync(FixtureModel model);

        /// <summary>
        /// Gets the list of matches.
        /// </summary>
        /// <param name="fixtureId">Fixture Id.</param>
        /// <returns>Returns the list of matches.</returns>
        Task<List<MatchModel>> GetMatchesAsync(Guid fixtureId);

        /// <summary>
        /// Uploads score sheet to Azure Blob Storage.
        /// </summary>
        /// <param name="request"><see cref="BlobUploadRequest"/> instance.</param>
        /// <returns>Returns the <see cref="BlobUploadResponse"/> instance.</returns>
        Task<BlobUploadResponse> UploadScoreSheetAsync(BlobUploadRequest request);
    }
}