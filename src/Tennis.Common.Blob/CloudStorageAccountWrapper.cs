using System;

using Microsoft.WindowsAzure.Storage;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="CloudStorageAccount"/> class.
    /// </summary>
    public class CloudStorageAccountWrapper : ICloudStorageAccountWrapper
    {
        private readonly CloudStorageAccount _sa;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="CloudStorageAccountWrapper"/> class.
        /// </summary>
        /// <param name="sa"><see cref="CloudStorageAccount"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sa"/> is <see langword="null" />.</exception>
        public CloudStorageAccountWrapper(CloudStorageAccount sa)
        {
            if (sa == null)
            {
                throw new ArgumentNullException(nameof(sa));
            }

            this._sa = sa;
        }

        /// <summary>
        /// Gets the blob endpoint Uri.
        /// </summary>
        public Uri BlobEndPoint => this._sa.BlobEndpoint;

        /// <summary>
        /// Gets the blob storage Uri
        /// </summary>
        public StorageUri BlobStorageUri => this._sa.BlobStorageUri;

        /// <summary>
        /// Tries to parse connection string to <see cref="CloudStorageAccountWrapper"/> instance.
        /// </summary>
        /// <param name="connectionString">Azure blob storage connection string.</param>
        /// <param name="account"><see cref="ICloudStorageAccountWrapper"/> instance parsed.</param>
        /// <returns>Returns <c>True</c>, if parsing is successful; otherwise returns <c>False</c>.</returns>
        public static bool TryParse(string connectionString, out ICloudStorageAccountWrapper account)
        {
            account = null;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return false;
            }

            CloudStorageAccount sa;
            if (!CloudStorageAccount.TryParse(connectionString, out sa))
            {
                return false;
            }

            account = new CloudStorageAccountWrapper(sa);
            return true;
        }

        /// <summary>
        /// Creates the <see cref="ICloudBlobClientWrapper"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="ICloudBlobClientWrapper"/> instance created.</returns>
        public ICloudBlobClientWrapper CreateCloudBlobClient()
        {
            var client = this._sa.CreateCloudBlobClient();
            return new CloudBlobClientWrapper(client);
        }

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
