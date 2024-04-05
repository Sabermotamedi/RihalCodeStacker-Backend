using Rihal.ReelRise.Application.Movies.Queries;

namespace Rihal.ReelRise.Application.FunctionalTests.Movies.Queries;
using static Testing;

public class SearchMovieTests : BaseTestFixture
{

    [Test]
    public async Task Should_Return_Movie_On_Search()
    {
        await RunAsDefaultUserAsync();

        var query = new SearchMovieQuery() { SearchValue = "Mad Max" };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.First(x=>x.Id==1).Id.Should().Be(1);
        result.First(x=>x.Id==1).Name.Should().BeEquivalentTo("Mad Max");
    }

}
