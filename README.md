
Based on the Visual Studio Unit Test framework, this library provides helper methods to speed up the development of new Unit Tests

<h2>How to use it</h2>

To access to the methods provided by the library, the test classes must inherit from *BaseTestClass*

```c#
[TestClass]
public class UserRepositoryTest : BaseTestClass
{
    ...
}
```
<h4>Methods</h4>

<h6>TestExpectedArgumentException</h6>

The method allows to verify that an exception, inheriting from ArgumentException, is thrown during the execution of the Test logic.
In the following example the code will verify that is not accepted a null value for the *user* parameter in the *Insert* method.

```c#
[TestMethod]
public void WhenUserIsNullItWillFail()
{
  TestExpectedArgumentException<ArgumentNullException>( () => new UserRepository().Insert(null), "user");
}
```

You can also pass directly an ArgumentNullException instance

```c#
[TestMethod]
public void WhenUserIsNullItWillFail()
{
  TestExpectedArgumentException( () => new UserRepository().Insert(null), new ArgumentNullException("user"));
}
```
<h6>TestExpectedException</h6>

The method works in the same way of the above method, except that is designed to work with any other type of exception.

```c#
[TestMethod]
public void WhenUserIsNullItWillFail()
{
  TestExpectedException<UserNotFoundException>( () => new UserRepository().Select(100),
                                                      "No user with ID 100 has been found.");
}
```
