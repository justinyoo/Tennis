using System;

namespace Tennis.Common.Blob
{
    /// <summary>
    /// This represents the entity for the blob upload response.
    /// </summary>
    public class BlobUploadResponse
    {
        /// <summary>
        /// Gets or sets the blob name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the blob URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the date when the blob has been uploaded.
        /// </summary>
        public DateTimeOffset DateUploaded { get; set; }
    }
}
