using System.Data;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Domain.Entities;
using Rihal.ReelRise.Domain.ValueObjects;

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
