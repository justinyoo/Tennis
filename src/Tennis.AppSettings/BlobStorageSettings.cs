namespace Tennis.AppSettings
{
    /// <summary>
    /// This represents the model entity for Azure Blob Storage
    /// </summary>
    public class BlobStorageSettings
    {
        /// <summary>
        /// Gets or sets the storage account name.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the blob storage container name.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the blob container URL.
        /// </summary>
        public string ContainerUrl { get; set; }
    }
}
