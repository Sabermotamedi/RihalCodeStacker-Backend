using NUnit.Framework;
using Rihal.ReelRise.Infrastructure.Service;
using System.Collections.Generic;

[TestFixture]
public class UrlExtractorTests
{
    private TextService extractor;

    [SetUp]
    public void Setup()
    {
        extractor = new TextService();
    }

    [Test]
    public void ExtractUrls_WhenTextContainsSingleUrl_ShouldReturnUrl()
    {
        // Arrange
        string text = "Here is a link to OpenAI's website: https://openai.com";
        List<string> expectedUrls = new List<string> { "https://openai.com" };

        // Act
        List<string> actualUrls = extractor.ExtractUrls(text);

        // Assert
        CollectionAssert.AreEqual(expectedUrls, actualUrls);
    }

    [Test]
    public void ExtractUrls_WhenTextContainsMultipleUrls_ShouldReturnUrls()
    {
        // Arrange
        string text = "Here are links to OpenAI's website: https://openai.com and to Google: https://google.com";
        List<string> expectedUrls = new List<string> { "https://openai.com", "https://google.com" };

        // Act
        List<string> actualUrls = extractor.ExtractUrls(text);

        // Assert
        CollectionAssert.AreEqual(expectedUrls, actualUrls);
    }

    [Test]
    public void ExtractUrls_WhenTextContainsNoUrl_ShouldReturnEmptyList()
    {
        // Arrange
        string text = "This text does not contain any URL";

        // Act
        List<string> actualUrls = extractor.ExtractUrls(text);

        // Assert
        CollectionAssert.IsEmpty(actualUrls);
    }

    [Test]
    public void ExtractUrls_WhenTextContainsUrlInDifferentFormats_ShouldReturnUrls()
    {
        // Arrange
        string text = "Here is a link to OpenAI's website: https://openai.com and to Google: http://www.google.com";
        List<string> expectedUrls = new List<string> { "https://openai.com", "http://www.google.com" };

        // Act
        List<string> actualUrls = extractor.ExtractUrls(text);

        // Assert
        CollectionAssert.AreEqual(expectedUrls, actualUrls);
    }

    [Test]
    public void ExtractUrls_WhenTextContainsUrlWithQueryString_ShouldReturnUrl()
    {
        // Arrange
        string text = "Here is a link to OpenAI's blog post: https://openai.com/blog/post?id=12345";
        List<string> expectedUrls = new List<string> { "https://openai.com/blog/post?id=12345" };

        // Act
        List<string> actualUrls = extractor.ExtractUrls(text);

        // Assert
        CollectionAssert.AreEqual(expectedUrls, actualUrls);
    }
}
