using Algorithms.DataCompression;

namespace Algorithms.Tests.Compressors;

public static class TranslatorTests
{
    [Test]
    public static void TranslateCorrectly()
    {
        // Arrange
        var translator = new Translator();
        var dict = new Dictionary<string, string>
        {
            { "Hey", "Good day" },
            { " ", " " },
            { "man", "sir" },
            { "!", "." },
        };

        // Act
        var translatedText = translator.Translate("Hey man!", dict);

        // Assert
        Assert.That(translatedText, Is.EqualTo("Good day sir."));
    }
}
