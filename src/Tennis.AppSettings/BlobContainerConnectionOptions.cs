using System;

using Tennis.Common.Blob;

namespace Tennis.AppSettings
{
    /// <summary>
    /// This represents the options entity for Azure Blob Storage Container connection.
    /// </summary>
    public class BlobContainerConnectionOptions : IBlobContainerConnectionOptions
    {
        private bool _disposed;

        /// <summary>
        /// Initializes default connection parameters
        /// </summary>
        /// <param name="connectionString"><see cref="ConnectionStringSettings"/> instance.</param>
        /// <param name="blobStorage"><see cref="BlobStorageSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connectionString"/> is <see langword="null" />.</exception>
        public BlobContainerConnectionOptions(ConnectionStringSettings connectionString, BlobStorageSettings blobStorage)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.BlobConnectionString = connectionString.ConnectionString;

            if (blobStorage == null)
            {
                throw new ArgumentNullException(nameof(blobStorage));
            }

            this.StorageAccountName = blobStorage.AccountName;
            this.BlobContainerName = blobStorage.ContainerName;
            this.BlobContainerUrl = blobStorage.ContainerUrl;
        }

        /// <summary>
        /// Gets the blob storage connection string.
        /// </summary>
        public string BlobConnectionString { get; }

        /// <summary>
        /// Gets the blob storage account name.
        /// </summary>
        public string StorageAccountName { get; }

        /// <summary>
        /// Gets the blob storage container name.
        /// </summary>
        public string BlobContainerName { get; }

        /// <summary>
        /// Gets the blob URL.
        /// </summary>
        public string BlobContainerUrl { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}