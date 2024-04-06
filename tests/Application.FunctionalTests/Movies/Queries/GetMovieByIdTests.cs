using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rihal.ReelRise.Application.Movies.Queries;

namespace Rihal.ReelRise.Application.FunctionalTests.Movies.Queries;
using static Testing;

public class GetMovieByIdTests : BaseTestFixture
{

    [Test]
    public async Task ShouldReturnMovieById()
    {
        await RunAsDefaultUserAsync();

        var query = new GetMovieByIdQuery() { Id = 1 };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Mad Max");
    }

}
