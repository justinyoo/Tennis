//namespace Tennis.Common.Blob
//{
//    /// <summary>
//    /// This represents the options entity for Azure Blob Storage Container connection.
//    /// </summary>
//    public class BlobContainerConnectionOptions : IBlobContainerConnectionOptions
//    {
//        private const string BlobConnectionStringKey = "UseDevelopmentStorage=true;";
//        private const string BlobStorageAccountNameKey = "BlobStorageAccount";
//        private const string BlobContainerNameKey = "filterpackages";
//        private const string BlobContainerUrlKey = "https://{0}.blob.core.windows.net/{1}";

//        /// <summary>
//        /// Initializes default connection parameters
//        /// </summary>
//        public BlobContainerConnectionOptions()
//        {
//            this.BlobConnectionString = GetBlobConnectionString();
//            this.StorageAccountName = GetStorageAccountName();
//            this.BlobContainerName = GetBlobContainerName();
//            this.BlobContainerUrl = GetBlobContainerUrl();
//        }

//        /// <summary>
//        /// Gets the blob storage connection string.
//        /// </summary>
//        public virtual string BlobConnectionString { get; }

//        /// <summary>
//        /// Gets the blob storage account name.
//        /// </summary>
//        public virtual string StorageAccountName { get; }

//        /// <summary>
//        /// Gets the blob storage container name.
//        /// </summary>
//        public virtual string BlobContainerName { get; }

//        /// <summary>
//        /// Gets the blob URL.
//        /// </summary>
//        public virtual string BlobContainerUrl { get; }

//        private static string GetBlobConnectionString()
//        {
//            var connString = ConfigurationManager.ConnectionStrings?["BlobContext"]?.ConnectionString ?? BlobConnectionStringKey;
//            return connString;
//        }

//        private static string GetStorageAccountName()
//        {
//            var accountName = ConfigurationManager.AppSettings?["StorageAccountName"] ?? BlobStorageAccountNameKey;
//            return accountName;
//        }

//        private static string GetBlobContainerName()
//        {
//            var containerName = ConfigurationManager.AppSettings?["BlobContainer"] ?? BlobContainerNameKey;
//            return containerName;
//        }

//        private static string GetBlobContainerUrl()
//        {
//            var accountName = ConfigurationManager.AppSettings?["BlobContainerUrl"] ?? BlobContainerUrlKey;
//            return accountName;
//        }
//    }
//}