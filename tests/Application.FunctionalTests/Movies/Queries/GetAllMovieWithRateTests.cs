using System.Data;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Domain.Entities;
using Rihal.ReelRise.Domain.ValueObjects;

namespace Rihal.ReelRise.Application.FunctionalTests.TodoLists.Queries;

using static Testing;

public class GetAllMovieWithRateTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllMovies()
    {
        await RunAsDefaultUserAsync();

        var query = new GetAllMovieWithRateQuery();

        var result = await SendAsync(query);

        result.Should().HaveCount(50);
    }

    private List<Movie> GetMockDatas()
    {
        return new List<Movie>
        {
            new Movie { Id = 1, Name = "Mad Max", Description = "Mad Max is a dystopian action film set in a post-apocalyptic world..." },
            new Movie { Id = 2, Name = "The Imitation Game", Description = "The Imitation Game is a biographical drama that portrays the life of Alan Turing..." },
            new Movie { Id = 3, Name = "The Shawshank Redemption", Description = "The Shawshank Redemption is a powerful drama that follows the story of Andy Dufresne..." },
            new Movie { Id = 4, Name = "Inception", Description = "Inception is a mind-bending science fiction thriller that delves into the world of dream infiltration..." },
            new Movie { Id = 5, Name = "Fight Club", Description = "Fight Club is a psychological drama that follows an insomniac office worker..." },
            new Movie { Id = 6, Name = "Saving Private Ryan", Description = "Saving Private Ryan is a gripping war film that follows a group of soldiers..." },
            new Movie { Id = 7, Name = "Parasite", Description = "Parasite is a darkly comedic thriller that explores class discrimination through..." }
        };
    }

    private Movie GetMockData()
    {
        return new Movie { Id = 140, Name = "Mad Max", Description = "Mad Max is a dystopian action film set in a post-apocalyptic world..." };
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetAllMovieWithRateQuery();

        var action = () => SendAsync(query);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
