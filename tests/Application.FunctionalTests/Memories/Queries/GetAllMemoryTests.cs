using Rihal.ReelRise.Application.Movies.Queries;

namespace Rihal.ReelRise.Application.FunctionalTests.Memories.Queries;

using static Testing;

public class GetAllMemoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturn_AllMemories_Without_Photo()
    {
        await RunAsDefaultUserAsync();

        var query = new GetAllMemoryQuery();

        var result = await SendAsync(query);

        result.Should().BeNullOrEmpty();
    }
}
