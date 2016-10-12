using System;

using Microsoft.WindowsAzure.Storage;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This provides interfaces to the <see cref="CloudStorageAccountWrapper"/> class.
    /// </summary>
    public interface ICloudStorageAccountWrapper : IDisposable
    {
        /// <summary>
        /// Gets the blob endpoint Uri.
        /// </summary>
        Uri BlobEndPoint { get; }

        /// <summary>
        /// Gets the blob storage Uri
        /// </summary>
        StorageUri BlobStorageUri { get; }

        /// <summary>
        /// Creates the <see cref="ICloudBlobClientWrapper"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="ICloudBlobClientWrapper"/> instance created.</returns>
        ICloudBlobClientWrapper CreateCloudBlobClient();
    }
}