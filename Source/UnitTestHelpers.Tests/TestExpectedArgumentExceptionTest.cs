namespace UnitTestHelpers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TestExpectedArgumentExceptionTest : BaseTestClass
    {
        [TestMethod]
        public void TestExpectedArgumentExceptionWithArgumentExceptionNotValidRaised()
        {
            try
            {
                Action<string> func = str => { throw new ArgumentException("The input string should have a value", "str"); };

                TestExpectedArgumentException<ArgumentException>(() => func(string.Empty), "The input string should have a value\r\nParameter name: str");
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual("Assert.AreEqual failed. Expected:<Test>. Actual:<Value does not fall within the expected range.>. ", ex.Message);
            }
        }

        [TestMethod]
        public void TestExpectedArgumentExceptionWithArgumentExceptionRaised()
        {
            TestExpectedArgumentException<ArgumentException>(() => { throw new ArgumentException("Test"); }, "Test");
        }

        [TestMethod]
        public void TestExpectedArgumentExceptionWithArgumentNullExceptionRaised()
        {
            Action<object> func = obj => { throw new ArgumentNullException("obj"); };

            TestExpectedArgumentException<ArgumentNullException>(() => func(null), "obj");
        }

        [TestMethod]
        public void TestExpectedArgumentExceptionWithNoExceptionRaised()
        {
            try
            {
                TestExpectedArgumentException<ArgumentException>(() => { }, "Test");
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
                TestExpectedArgumentException<ArgumentException>(null, string.Empty);
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
                TestExpectedArgumentException<ArgumentException>(() => { }, string.Empty);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("The parameter name cannot be null, empty or composed only by spaces.\r\nParameter name: parameterName", ex.Message);
            }
        }
    }
}