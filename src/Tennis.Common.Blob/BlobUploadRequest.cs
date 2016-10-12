using System.IO;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This represents the entity for Azure Blob Storage upload request.
    /// </summary>
    public class BlobUploadRequest
    {
        /// <summary>
        /// Gets or sets the blob name.
        /// </summary>
        public string BlobName { get; set; }

        /// <summary>
        /// Gets or sets the content type of the blob name.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the blob stream to upload.
        /// </summary>
        public Stream Stream { get; set; }
    }
}