using System;
using System.Runtime.Serialization;

namespace Tennis.Common.Blob.Exceptions
{
    /// <summary>
    /// This represents the exception entity thrown when the blob cannot be found.
    /// </summary>
    public class BlobNotFoundException : ApplicationException
    {
        private const string BlobNotFoundMessage = "Blob not found";

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobNotFoundException"/> class.
        /// </summary>
        public BlobNotFoundException()
            : this(BlobNotFoundMessage)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        public BlobNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobNotFoundException"/> class.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
        public BlobNotFoundException(Exception innerException)
            : this(BlobNotFoundMessage, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
        public BlobNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BlobNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected BlobNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}