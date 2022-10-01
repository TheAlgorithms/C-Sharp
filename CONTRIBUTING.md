# Contributing
## General notes
When contributing to this repository, if your changes are subjective, controversial or people are likely to have
polarized opinions on this matter, please first discuss the change you wish to make via issue with the owners of
this repository.

We welcome adding new algorithms and data structures that were mentioned in books or other reputable sources.
We also welcome fixing bugs in code, clarifying documentation and adding new test cases to check existing code.
The framework targeted by our code is **dotnet 6**. The corresponding SDK can be found [here](https://dotnet.microsoft.com/download/dotnet/6.0).

Please note that we have a code of conduct, please follow it in all your interactions with the project.

## Files
For adding new algorithms, please ensure to name the cs-files corresponding to the classname, e.g. `Factorial.cs` for the class `Factorial` and add them to the most relevant pre-existing folder. Make sure to implement tests for all public methods. These tests should be added in a separate cs-file to the corresponding folder in `Algorithms.Tests` and have their classname ending in "Test", e.g. `FactorialTest`.

## Tests
We use the [NUnit-library](https://nunit.org/) for testing. Instructions for the installation for local testing can be found [here](https://docs.nunit.org/articles/nunit/getting-started/installation.html). A basic test can be implemented by adding the attribute `[Test]` in front of the method performing the test and including an Assert-statement within the method, e.g. `Assert.AreEqual(result, 42)`. If possible, please use [FluentAssertions](https://fluentassertions.com/) since they are more eloquent and readable. Some of the basic assertions can be found [here](https://fluentassertions.com/basicassertions/). For getting familiar with the Nunit-tests, it might be helpful to have a look at some existing test-files. A tutorial explaining how to implement and run NUnit-tests can be found [here](https://www.c-sharpcorner.com/article/introduction-to-nunit-testing-framework/).

## Automatic checks
One of the automatic checks we use is [codecov](https://about.codecov.io/). It checks whether each conditional branch is covered by a test. So if a method contains a conditional statement or operator (if-else, switch, ?: ) then there should be at least a test per branch (unless all the branches can be covered in one run).

The coding style follows the default code formatter of Visual Studio.

## Comments
Please use the [XML documentation features](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/how-to-use-the-xml-documentation-features) for comments. The comments should include a summary of the class/method and an explanation of the different parameters and of the return value. Including a link to Wikipedia or to another source of information on the algorithm is encouraged.
