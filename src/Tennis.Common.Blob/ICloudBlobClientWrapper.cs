using System;

using Microsoft.WindowsAzure.Storage.Blob;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This provides interfaces to the <see cref="CloudBlobClientWrapper"/> class.
    /// </summary>
    public interface ICloudBlobClientWrapper : IDisposable
    {
        /// <summary>
        /// Gets the base Uri of the client.
        /// </summary>
        Uri BaseUri { get; }

        /// <summary>
        /// Gets the <see cref="CloudBlobContainer"/> instance.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <returns>Returns the <see cref="CloudBlobContainer"/> instance.</returns>
        CloudBlobContainer GetContainerReference(string containerName);
    }
}