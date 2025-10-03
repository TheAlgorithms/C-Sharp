using Algorithms.Other;

namespace Algorithms.Tests.Other;

/// <summary>
///     A class for testing the Luhn algorithm.
/// </summary>
public class LuhnTests
{
    [TestCase("89014103211118510720")] // ICCID
    [TestCase("071052120")] // Social Security Code
    [TestCase("449125546588769")] // IMEI
    [TestCase("4417123456789113")] // Bank card
    public void ValidateTrue(string number)
    {
        // Arrange
        bool validate;

        // Act
        validate = Luhn.Validate(number);

        // Assert
        Assert.That(validate, Is.True);
    }

    [TestCase("89012104211118510720")] // ICCID
    [TestCase("021053120")] // Social Security Code
    [TestCase("449145545588969")] // IMEI
    [TestCase("4437113456749113")] // Bank card
    public void ValidateFalse(string number)
    {
        // Arrange
        bool validate;

        // Act
        validate = Luhn.Validate(number);

        // Assert
        Assert.That(validate, Is.False);
    }

    [TestCase("x9012104211118510720")] // ICCID
    [TestCase("0210x3120")] // Social Security Code
    [TestCase("44914554558896x")] // IMEI
    [TestCase("4437113456x49113")] // Bank card
    public void GetLostNum(string number)
    {
        // Arrange
        int lostNum;
        bool validate;

        // Act
        lostNum = Luhn.GetLostNum(number);
        validate = Luhn.Validate(number.Replace("x", lostNum.ToString()));

        // Assert
        Assert.That(validate, Is.True);
    }

    [TestCase("")]
    [TestCase("xxxx")] 
    [TestCase("abcde")]
    [TestCase("1x345678901234567")]
    [TestCase("x1234567890123456")]
    [TestCase("1234567890123456x")]
    [TestCase("1111111111111111")]
    [TestCase("1a2b3c4d5e6f7g8h9i0j")]
    public void EdgeCases_GetLostNum(string number)
    {
        // Act
        int lostNum = Luhn.GetLostNum(number.Replace("x", "0"));
        // Assert
        Assert.That(lostNum, Is.InRange(0, 9));
    }

    [TestCase("1a2b3c4d5e6f7g8h9i0j")]
    public void EdgeCases_Validate(string number)
    {
        // Act
        bool result = Luhn.Validate(number);
        // Assert
        Assert.That(result, Is.False);
    }
}
