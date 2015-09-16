using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestHelpers.Tests
{
    [TestClass]
    public class NoExceptionRaisedExceptionTest : BaseTestClass
    {
        private readonly Type exceptionType = typeof(ArgumentException);
        private const string ExceptionMessage = "Test";

        [TestMethod]
        public void InstanceWithValidType()
        {
            var noExceptionRaisedException = new NoExceptionRaisedException(exceptionType);

            AssertExpectedExceptionType(noExceptionRaisedException);
        }

        [TestMethod]
        public void InstanceWithValidTypeAndMessage()
        {
            var noExceptionRaisedException = new NoExceptionRaisedException(exceptionType, ExceptionMessage);

            AssertExpectedExceptionType(noExceptionRaisedException);
            AssertExceptionMessage(noExceptionRaisedException);
        }

        [TestMethod]
        public void InstanceWithValidTypeAndMessageAndInnerException()
        {
            var innerException = new Exception();
            var noExceptionRaisedException = new NoExceptionRaisedException(exceptionType,
                ExceptionMessage,
                innerException);

            AssertExpectedExceptionType(noExceptionRaisedException);
            AssertExceptionMessage(noExceptionRaisedException);
            Assert.AreEqual(innerException, noExceptionRaisedException.InnerException);
        }

        private static void AssertExceptionMessage(NoExceptionRaisedException noExceptionRaisedException)
        {
            Assert.AreEqual(ExceptionMessage, noExceptionRaisedException.Message);
        }

        private void AssertExpectedExceptionType(NoExceptionRaisedException noExceptionRaisedException)
        {
            Assert.AreEqual(exceptionType, noExceptionRaisedException.ExpectedExceptionType);
        }

        [TestMethod]
        public void WhenExpectedExceptionTypeIsInvalidItWillFail()
        {
            TestExpectedArgumentException<ArgumentNullException>(() => new NoExceptionRaisedException(null), "expectedExceptionType");
        }

        [TestMethod]
        public void WhenExpectedExceptionTypeIsNotAnExceptionItWillFail()
        {
            TestExpectedArgumentException<ArgumentException>(() => new NoExceptionRaisedException(typeof(object)),
                Strings.NoExceptionRaisedExceptionTypeInvalid);
        }
    }
}