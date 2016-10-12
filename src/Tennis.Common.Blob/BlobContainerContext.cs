using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Blob;

using Tennis.Common.Blob.Exceptions;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This represents the context entity for Azure Blob Storage Container.
    /// </summary>
    public class BlobContainerContext : IBlobContainerContext
    {
        private readonly IBlobContainerConnection _connection;

        private CloudBlobContainer _container;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobContainerContext"/> class.
        /// </summary>
        /// <param name="connection"><see cref="IBlobContainerConnection"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is <see langword="null" />.</exception>
        public BlobContainerContext(IBlobContainerConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            this._connection = connection;
        }

        /// <summary>
        /// Gets the <see cref="CloudBlobContainer"/> reference instance.
        /// </summary>
        public CloudBlobContainer BlobContainerReference
            => this._container ?? (this._container = this._connection.GetBlobContainerReference());

        /// <summary>
        /// Uploads file to blob container.
        /// </summary>
        /// <param name="request"><see cref="BlobUploadRequest"/> instance.</param>
        /// <returns>Returns the <see cref="BlobUploadResponse"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="request"/> is <see langword="null" />.</exception>
        /// <exception cref="BlobNotFoundException">Blob reference doesn't exist.</exception>
        public async Task<BlobUploadResponse> UploadAsync(BlobUploadRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                var exists = await this._connection.EnsureBlobContainerExistsAsync().ConfigureAwait(false);
                if (!exists)
                {
                    throw new BlobContainerNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            try
            {
                var blob = this.BlobContainerReference.GetBlockBlobReference(request.BlobName);
                await blob.UploadFromStreamAsync(request.Stream).ConfigureAwait(false);

                blob.Properties.ContentType = request.ContentType;
                await blob.SetPropertiesAsync().ConfigureAwait(false);

                var result = new BlobUploadResponse()
                             {
                                 Name = blob.Name,
                                 Url = blob.Uri.ToString(),
                                 DateUploaded = blob.Properties.LastModified.GetValueOrDefault()
                             };

                return result;
            }
            catch (ArgumentNullException ex)
            {
                throw new BlobNotFoundException(ex);
            }
            catch (ArgumentException ex)
            {
                throw new BlobNotFoundException(ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads file from blob container to local file system.
        /// </summary>
        /// <param name="blobname">Blob name.</param>
        /// <param name="filepath">Fully qualified file path to download.</param>
        /// <exception cref="ArgumentNullException"><paramref name="blobname"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="filepath"/> is <see langword="null" />.</exception>
        /// <exception cref="BlobContainerNotFoundException">Blob container doesn't exist.</exception>
        /// <exception cref="BlobNotFoundException">Blob reference doesn't exist.</exception>
        public async Task DownloadAsync(string blobname, string filepath)
        {
            if (string.IsNullOrWhiteSpace(blobname))
            {
                throw new ArgumentNullException(nameof(blobname));
            }

            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentNullException(nameof(filepath));
            }

            var exists = await this._connection.EnsureBlobContainerExistsAsync().ConfigureAwait(false);
            if (!exists)
            {
                throw new BlobContainerNotFoundException();
            }

            try
            {
                var blob = this.BlobContainerReference.GetBlockBlobReference(blobname);
                await blob.DownloadToFileAsync(filepath, FileMode.Create).ConfigureAwait(false);
            }
            catch (ArgumentNullException)
            {
                throw new BlobNotFoundException();
            }
            catch (ArgumentException)
            {
                throw new BlobNotFoundException();
            }
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