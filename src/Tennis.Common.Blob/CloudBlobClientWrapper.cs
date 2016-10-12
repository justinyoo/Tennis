using System;

using Microsoft.WindowsAzure.Storage.Blob;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="CloudBlobClient"/> class.
    /// </summary>
    public class CloudBlobClientWrapper : ICloudBlobClientWrapper
    {
        private readonly CloudBlobClient _client;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of th e<see cref="CloudBlobClientWrapper"/> class.
        /// </summary>
        /// <param name="client"><see cref="CloudBlobClient"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="client"/> is <see langword="null" />.</exception>
        public CloudBlobClientWrapper(CloudBlobClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this._client = client;
        }

        /// <summary>
        /// Gets the base Uri of the client.
        /// </summary>
        public Uri BaseUri => this._client.BaseUri;

        /// <summary>
        /// Gets the <see cref="CloudBlobContainer"/> instance.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <returns>Returns the <see cref="CloudBlobContainer"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="containerName"/> is <see langword="null" />.</exception>
        public CloudBlobContainer GetContainerReference(string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(nameof(containerName));
            }

            return this._client.GetContainerReference(containerName);
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