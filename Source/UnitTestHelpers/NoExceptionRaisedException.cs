using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace UnitTestHelpers
{
    /// <summary>
    /// This exception is raised when a specif exception type is expected
    /// but it's not thrown.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"),
    SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"),
    Serializable]
    public class NoExceptionRaisedException : Exception
    {
        /// <summary>
        /// Gets or sets the expected type of the exception.
        /// </summary>
        /// <value>
        /// The expected type of the exception.
        /// </value>
        public Type ExpectedExceptionType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoExceptionRaisedException"/> class.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <exception cref="System.ArgumentNullException">
        /// expectedExceptionType
        /// or
        /// expectedExceptionType
        /// </exception>
        public NoExceptionRaisedException(Type expectedExceptionType)
            : this(expectedExceptionType, Strings.NoExceptionRaisedExceptionDefaultMessage)
        {
        }

        private static void ValidateExpectedExceptionType(Type expectedExceptionType)
        {
            if (expectedExceptionType == null)
            {
                throw new ArgumentNullException("expectedExceptionType");
            }

            if (!expectedExceptionType.IsSubclassOf(typeof(Exception)))
            {
                throw new ArgumentException(Strings.NoExceptionRaisedExceptionTypeInvalid);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoExceptionRaisedException"/> class.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <param name="message">The message.</param>
        public NoExceptionRaisedException(Type expectedExceptionType, string message)
            : this(expectedExceptionType, message, default(Exception))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoExceptionRaisedException"/> class.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NoExceptionRaisedException(Type expectedExceptionType, string message, Exception innerException)
            : base(message, innerException)
        {
            ValidateExpectedExceptionType(expectedExceptionType);

            ExpectedExceptionType = expectedExceptionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoExceptionRaisedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        [ExcludeFromCodeCoverage]
        protected NoExceptionRaisedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}