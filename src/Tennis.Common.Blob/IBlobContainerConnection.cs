using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Blob;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This provides interfaces to the <see cref="BlobContainerConnection"/> class.
    /// </summary>
    public interface IBlobContainerConnection : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="CloudBlobContainer"/> reference instance.
        /// </summary>
        /// <returns>Returns the <see cref="CloudBlobContainer"/> reference instance.</returns>
        CloudBlobContainer GetBlobContainerReference();

        /// <summary>
        /// Ensures whether the blob container actually exists or not.
        /// </summary>
        /// <returns>Returns <c>True</c>, if it exists; otherwise returns <c>False</c>.</returns>
        Task<bool> EnsureBlobContainerExistsAsync();

        /// <summary>
        /// Creates the blob container.
        /// </summary>
        Task CreateBlobContainerAsync();

        /// <summary>
        /// Deletes the blob container.
        /// </summary>
        Task DeleteBlobContainerAsync();

        /// <summary>
        /// Clears blob container reference.
        /// </summary>
        void ClearBlobContainerReference();
    }
}
