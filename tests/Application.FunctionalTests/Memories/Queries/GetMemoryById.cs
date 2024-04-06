using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.Movies.Queries;

namespace Rihal.ReelRise.Application.FunctionalTests.Memories.Queries;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Testing;

public class GetAllMemoryTests : BaseTestFixture
{
    [Test]
    public async Task Should_Save_GetAll_And_ReturnAllMemories_WithPhoto_Information()
    {
        await RunAsDefaultUserAsync();

        var request = new CreateMemoryCommand
        {
            MovieId = 1,
            Title = "Test Memory",
            Story = "Test Story",
            SawDate = DateTime.UtcNow,
            Photos = new FormFileCollection
                {
                    new FormFile(Stream.Null, 0, 0, "testPhotoA.jpg", "testPhotoA.jpg"),
                    new FormFile(Stream.Null, 0, 0, "testPhotoB.jpg", "testPhotoB.jpg")
                }
        };

        await SendAsync(request);


        var queryAll = new GetAllMemoryQuery();

        var allMemory = await SendAsync(queryAll);

        var queryById = new GetMemoryByIdQuery() { Id = allMemory.First().Id };

        var result = await SendAsync(queryById);

        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be("Test Memory");
        result.MemoryPhotos.Should().NotBeNullOrEmpty();
        result.MemoryPhotos.Should().HaveCount(2);
        result.MemoryPhotos?[0].Name.Should().Be("testPhotoA");
        result.MemoryPhotos?[0].Extension.Should().Be("jpg");
        result.MemoryPhotos?[0].Size.Should().Be(0);

    }
}
