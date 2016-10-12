using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Blob;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// Interface to implement context for blob storage
    /// </summary>
    public interface IBlobContainerContext : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="CloudBlobContainer"/> reference instance.
        /// </summary>
        CloudBlobContainer BlobContainerReference { get; }

        /// <summary>
        /// Uploads file to blob container.
        /// </summary>
        /// <param name="request"><see cref="BlobUploadRequest"/> instance.</param>
        /// <returns>Returns the <see cref="BlobUploadResponse"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="request"/> is <see langword="null" />.</exception>
        Task<BlobUploadResponse> UploadAsync(BlobUploadRequest request);

        /// <summary>
        /// Downloads file from blob container to local file system.
        /// </summary>
        /// <param name="blobname">Blob name.</param>
        /// <param name="filepath">Fully qualified file path to download.</param>
        Task DownloadAsync(string blobname, string filepath);
    }
}