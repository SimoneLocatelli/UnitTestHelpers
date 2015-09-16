namespace UnitTestHelpers
{
    #region Usings

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    #endregion Usings

    /// <summary>
    /// Provides helpers methods to simplify the design of unit tests.
    /// </summary>
    public abstract class BaseTestClass
    {
        /// <summary>
        /// Verifies that the expected exception is thrown during the execution of the input lambda expression.
        /// </summary>
        /// <typeparam name="TException">The type of the exception expected.</typeparam>
        /// <param name="action">The action that will be executed.</param>
        /// <param name="exception">The exception instance initialized with the data, such as the message, that we expect to find
        /// in the exception we want to test.</param>
        /// <exception cref="System.ArgumentNullException">The action can't be null.</exception>
        /// <exception cref="System.ArgumentException">The input exception can't be null.</exception>
        /// <exception cref="UnitTestHelpers.NoExceptionRaisedException">Throw when the lambda expression succeed or it throws a different type of excpetion</exception>
        protected virtual void TestExpectedArgumentException<TException>(Action action, TException exception)
            where TException : ArgumentException
        {
            ValidateAction(action);

            ValidateException(exception);

            InternalTestExpectedException<TException>(action, ex => ValidateArgumentException(ex, exception.Message, exception.ParamName));
        }

        /// <summary>
        /// Verifies that the expected exception is thrown during the execution of the input lambda expression.
        /// </summary>
        /// <typeparam name="TException">The type of the exception expected.</typeparam>
        /// <param name="action">The action that will be executed.</param>
        /// <param name="parameterName">Name of the parameter contained in the ArgumentException.</param>
        /// <exception cref="System.ArgumentException">The input exception can't be null.</exception>
        /// <exception cref="System.ArgumentNullException">The action can't be null.</exception>
        /// <exception cref="UnitTestHelpers.NoExceptionRaisedException">Throw when the lambda expression succeed or it throws a different type of excpetion</exception>
        protected virtual void TestExpectedArgumentException<TException>(Action action, string parameterName)
            where TException : ArgumentException
        {
            ValidateAction(action);

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException(Strings.TestExpectedArgumentExceptionParameterNameInvalid, "parameterName");
            }

            InternalTestExpectedException<TException>(action, ex => ValidateArgumentException(ex, parameterName, parameterName));
        }

        /// <summary>
        /// Verifies that the expected exception is thrown during the execution of the input lambda expression.
        /// </summary>
        /// <typeparam name="TException">The type of the exception expected.</typeparam>
        /// <param name="action">The action that will be executed.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <exception cref="System.ArgumentException">The input exception can't be null.</exception>
        /// <exception cref="System.ArgumentNullException">The action can't be null.</exception>
        /// <exception cref="UnitTestHelpers.NoExceptionRaisedException">Throw when the lambda expression succeed or it throws a different type of excpetion</exception>
        protected virtual void TestExpectedException<TException>(Action action, string exceptionMessage) where TException : Exception
        {
            ValidateAction(action);

            if (string.IsNullOrWhiteSpace(exceptionMessage))
            {
                throw new ArgumentException(Strings.TestExpectedExceptionMessageInvalid, "exceptionMessage");
            }

            InternalTestExpectedException<TException>(action, ex => Assert.AreEqual(exceptionMessage, ex.Message));
        }

        /// <summary>
        /// Verifies that the expected exception is thrown during the execution of the input lambda expression.
        /// </summary>
        /// <typeparam name="TException">The type of the exception expected.</typeparam>
        /// <param name="action">The action that will be executed.</param>
        /// <param name="exception">The exception instance initialized with the data, such as the message, that we expect to find
        /// in the exception we want to test.</param>
        /// <exception cref="System.ArgumentNullException">The action can't be null.</exception>
        /// <exception cref="System.ArgumentException">The input exception can't be null.</exception>
        /// <exception cref="UnitTestHelpers.NoExceptionRaisedException">Throw when the lambda expression succeed or it throws a different type of excpetion</exception>
        protected virtual void TestExpectedException<TException>(Action action, TException exception) where TException : Exception
        {
            ValidateAction(action);

            ValidateException(exception);

            InternalTestExpectedException<TException>(action, ex => Assert.AreEqual(exception.Message, ex.Message));
        }

        /// <summary>
        /// Throws the no exception raised exception.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        private static void ThrowNoExceptionRaisedException<TException>()
            where TException : Exception
        {
            throw new NoExceptionRaisedException(typeof(TException));
        }

        private static void ValidateAction(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
        }

        private static void ValidateArgumentException<TException>(TException exception, string messageName, string parameterName) where TException : ArgumentException
        {
            if (exception is ArgumentNullException) Assert.AreEqual(parameterName, exception.ParamName);
            else Assert.AreEqual(messageName, exception.Message);
        }

        private static void ValidateException<TException>(TException exception) where TException : Exception
        {
            if (exception != null) return;
            throw new ArgumentException(Strings.TestExpectedExceptionExceptionInvalid, "exception");
        }

        private static void InternalTestExpectedException<TException>(Action action, Action<TException> verifyThrownException)
            where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TException));
                verifyThrownException(ex);
                return;
            }

            ThrowNoExceptionRaisedException<TException>();
        }
    }
}