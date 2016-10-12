using System;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This provides interfaces to blob container connection options classes.
    /// </summary>
    public interface IBlobContainerConnectionOptions : IDisposable
    {
        /// <summary>
        /// Gets the blob storage connection string.
        /// </summary>
        string BlobConnectionString { get; }

        /// <summary>
        /// Gets the blob storage account name.
        /// </summary>
        string StorageAccountName { get; }

        /// <summary>
        /// Gets the blob storage container name.
        /// </summary>
        string BlobContainerName { get; }

        /// <summary>
        /// Gets the blob URL.
        /// </summary>
        string BlobContainerUrl { get; }
    }
}