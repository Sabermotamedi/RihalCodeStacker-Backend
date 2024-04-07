using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Rihal.ReelRise.Infrastructure.Service;
using System.Collections.Generic;

[TestFixture]
public class MovieTests
{
    private TextService extractor;

    [SetUp]
    public void Setup()
    {
        extractor = new TextService();
    }


    [Test]
    [TestCase("eno", "Godzilla Minus One")]
    [TestCase("eund", "Dune")]
    [TestCase("iPvetar", "Saving Private Ryan")]
    [TestCase("S7ene", "Se7en")]
    [TestCase("plup", "Pulp Fiction")]
    [TestCase("plup Fiction", "Pulp Fiction")]
    [TestCase("plup nctiifo", "Pulp Fiction")]
    [TestCase("nctiifo", "Pulp Fiction")]
    [TestCase("raw ", "Avengers: Infinity War")]
    [TestCase("kjhsdjf", "No match found")]
    public void GuessMovieTitle_ScrambledInput_ShouldReturnCorrectTitle(string scrambled, string answer)
    {
        // Act
        var result = extractor.GuessMovieTitle(scrambled, movies);

        // Assert
        result.Name.Should().Be(answer);
    }

    private readonly static Dictionary<int, string> movies = new Dictionary<int, string>
{
    {1, "Godzilla Minus One"},
    {2, "The Shawshank Redemption"},
    {3, "Inception"},
    {4, "Fight Club"},
    {5, "Saving Private Ryan"},
    {6, "Parasite"},
    {7, "Django Unchained"},
    {8, "Mad Max"},
    {9, "The Imitation Game"},
    {10, "Se7en"},
    {11, "Spirited Away"},
    {12, "Jurassic Park"},
    {13, "Finding Nemo"},
    {14, "The Hobbit: An Unexpected Journey"},
    {15, "Shutter Island"},
    {16, "The Godfather"},
    {17, "Schindler's List"},
    {18, "Pulp Fiction"},
    {19, "Goodfellas"},
    {20, "Seven Samurai"},
    {21, "The Silence of the Lambs"},
    {22, "The Prestige"},
    {23, "Whiplash"},
    {24, "WALL·E"},
    {25, "Avengers: Infinity War"},
    {26, "The Dark Knight Rises"},
    {27, "Toy Story"},
    {28, "Braveheart"},
    {29, "Joker"},
    {30, "Dune"},
    {31, "3 Idiots"},
    {32, "The Hunt"},
    {33, "Lawrence of Arabia"},
    {34, "Up"},
    {35, "Indiana Jones and the Last Crusade"},
    {36, "1917"},
    {37, "Taxi Driver"},
    {38, "Dangal"},
    {39, "A Beautiful Mind"},
    {40, "King's Speech"},
    {41, "Prisoners"},
    {42, "Inside Out"},
    {43, "Fargo"},
    {44, "Catch Me If You Can"},
    {45, "My Neighbor Totoro"},
    {46, "Blade Runner"},
    {47, "The Grand Budapest Hotel"},
    {48, "Room"},
    {49, "Ford v Ferrari"},
    {50, "There Will Be Blood"}
};

}
