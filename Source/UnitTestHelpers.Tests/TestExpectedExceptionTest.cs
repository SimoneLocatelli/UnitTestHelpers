namespace UnitTestHelpers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TestExpectedExceptionTest : BaseTestClass
    {
        [TestMethod]
        public void TestExpectedExceptionWithValidExceptionRaised()
        {
            TestExpectedException<InvalidOperationException>(() => { throw new InvalidOperationException("Test"); }, "Test");
        }

        [TestMethod]
        public void TestExpectedExceptionWithInvalidExceptionRaised()
        {
            try
            {
                TestExpectedException<InvalidOperationException>(() => { throw new InvalidOperationException(); }, "Test");
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual("Assert.AreEqual failed. Expected:<Test>. Actual:<Operation is not valid due to the current state of the object.>. ", ex.Message);
            }
        }

        [TestMethod]
        public void TestExpectedExceptionWithNoExceptionRaised()
        {
            try
            {
                TestExpectedException<InvalidOperationException>(() => { }, "Test");
            }
            catch (NoExceptionRaisedException ex)
            {
                Assert.AreEqual(Strings.NoExceptionRaisedExceptionDefaultMessage, ex.Message);
            }
        }

        [TestMethod]
        public void TestExpectedArgumentExceptionWithNullAction()
        {
            try
            {
                TestExpectedException<InvalidOperationException>(null, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("action", ex.ParamName);
            }
        }

        [TestMethod]
        public void TestExpectedArgumentExceptionWithStringEmpty()
        {
            try
            {
                TestExpectedException<InvalidOperationException>(() => { }, string.Empty);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("The exception message cannot be null, empty or composed only by spaces.\r\nParameter name: exceptionMessage", ex.Message);
            }
        }
    }
}