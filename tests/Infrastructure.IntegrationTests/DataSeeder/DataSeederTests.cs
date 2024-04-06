using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Rihal.ReelRise.Infrastructure.Data;

namespace Rihal.ReelRise.Infrastructure.IntegrationTests.DataSeeder;
public class DataSeederTests
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly MovieDataSeeder _movieService;

    public DataSeederTests()
    {
        _webHostEnvironment = Mock.Of<IWebHostEnvironment>(w =>
            w.WebRootPath == "C:\\Repository\\MyProject\\RihalCodeStacker-Backend\\src\\Web\\wwwroot");

        _movieService = new MovieDataSeeder(_webHostEnvironment);
    }


    [Test]
    public async Task GetMovies_ReturnsListOfMovies()
    {
        // Act
        var result = await _movieService.GetMovies();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result?.Count.Should().Be(50);
    }
}
