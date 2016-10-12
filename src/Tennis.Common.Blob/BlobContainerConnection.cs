using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This represents the connection entity for the Azure Blob Storage Container.
    /// </summary>
    public class BlobContainerConnection : IBlobContainerConnection
    {
        private readonly IBlobContainerConnectionOptions _options;

        private CloudBlobContainer _container;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobContainerConnection"/> class.
        /// </summary>
        /// <param name="options"><see cref="IBlobContainerConnectionOptions"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null" />.</exception>
        public BlobContainerConnection(IBlobContainerConnectionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this._options = options;
        }

        /// <summary>
        /// Gets the <see cref="CloudBlobContainer"/> reference instance.
        /// </summary>
        /// <returns>Returns the <see cref="CloudBlobContainer"/> reference instance.</returns>
        public CloudBlobContainer GetBlobContainerReference()
        {
            if (this._container != null)
            {
                return this._container;
            }

            try
            {
                using (var account = this.GetStorageAccount(this._options.BlobConnectionString))
                using (var client = account.CreateCloudBlobClient())
                {
                    this._container = client.GetContainerReference(this._options.BlobContainerName);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return this._container;
        }

        /// <summary>
        /// Ensures whether the blob container actually exists or not.
        /// </summary>
        /// <returns>Returns <c>True</c>, if it exists; otherwise returns <c>False</c>.</returns>
        public async Task<bool> EnsureBlobContainerExistsAsync()
        {
            if (this._container == null)
            {
                this._container = this.GetBlobContainerReference();
            }

            bool exists;
            try
            {
                exists = await this._container.ExistsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }

            return exists;
        }

        /// <summary>
        /// Creates the blob container.
        /// </summary>
        public async Task CreateBlobContainerAsync()
        {
            var accessType = BlobContainerPublicAccessType.Off;
            var options = new BlobRequestOptions();
            var context = new OperationContext();

            try
            {
                var created = await this._container.CreateIfNotExistsAsync(accessType, options, context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the blob container.
        /// </summary>
        public async Task DeleteBlobContainerAsync()
        {
            try
            {
                var deleted = await this._container.DeleteIfExistsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Clears blob container reference.
        /// </summary>
        public void ClearBlobContainerReference()
        {
            this._container = null;
        }

        private ICloudStorageAccountWrapper GetStorageAccount(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            ICloudStorageAccountWrapper sa;
            var account = CloudStorageAccountWrapper.TryParse(connectionString, out sa) ? sa : null;
            return account;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value whether to perform disposing managed resources or not.</param>
        protected void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            this._container = null;

            this._disposed = true;
        }
    }
}