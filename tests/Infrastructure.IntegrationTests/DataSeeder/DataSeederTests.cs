using Newtonsoft.Json;
using Rihal.ReelRise.Domain.Entities;
using System.Reflection;

namespace Rihal.ReelRise.Infrastructure.IntegrationTests.DataSeeder;
public class DataSeederTests
{

    [Test]
    public async Task MovieJsonShouldBeAvailable()
    {
        // Arrange
        var movieService = new MovieService();

        // Act
        var movies = await movieService.GetMovies();

        // Assert
        Assert.IsNotNull(movies);
        Assert.IsTrue(movies.Count > 0);
    }

    public class MovieService
    {
        public async Task<List<Movie>?> GetMovies()
        {

            Assembly assembly = Assembly.Load("Infrastructure.dll");
            string assemblyPath01 = assembly.Location;

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string directoryPath = Path.GetDirectoryName(assemblyPath) ?? throw new InvalidOperationException("The directory path could not be determined.");

            string filePath = Path.Combine(directoryPath, "movies.json");

            string json = await File.ReadAllTextAsync(filePath);

            List<Movie>? movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }

    }

}
