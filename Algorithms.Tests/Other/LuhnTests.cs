using NUnit.Framework;
using Algorithms.Other;

namespace Algorithms.Tests.Other
{
    /// <summary>
    /// A class for testing the Luhn algorithm.
    /// </summary>
    public class LuhnTests
    {
        [Test]
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
            Assert.True(validate);
        }

        [Test]
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
            Assert.False(validate);
        }

        [Test]
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
            Assert.True(validate);
        }
    }
}
