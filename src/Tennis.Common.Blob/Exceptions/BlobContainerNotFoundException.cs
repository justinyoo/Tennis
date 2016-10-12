using System;
using System.Runtime.Serialization;

namespace Tennis.Common.Blob.Exceptions
{
    /// <summary>
    /// This represents the exception entity thrown when the blob container cannot be found.
    /// </summary>
    public class BlobContainerNotFoundException : ApplicationException
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="BlobContainerNotFoundException"/> class.
        /// </summary>
        public BlobContainerNotFoundException()
            : this("Blob container not found")
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobContainerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        public BlobContainerNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobContainerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
        public BlobContainerNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobContainerNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected BlobContainerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}