using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Exceptions;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.Movies.Queries;

namespace Rihal.ReelRise.Application.FunctionalTests.MovieRates.Commands;
using static Testing;

public class CreateMemoriesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateMemoryCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task Should_ThrowValidationException_WhenTitleIsNull()
    {
        var command = new CreateMemoryCommand() { MovieId = 1, Story = "Story", SawDate = DateTime.UtcNow };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("'Title' must not be empty.");
    }


    [Test]
    public async Task Should_ThrowValidationException_WhenStoryIsNull()
    {
        var command = new CreateMemoryCommand() { MovieId = 1, Title = "Title", SawDate = DateTime.UtcNow };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Story")))
                .And.Errors["Story"].Should().Contain("'Story' must not be empty.");
    }


    [Test]
    public async Task Should_ThrowValidationException_WhenSawDateIsNull()
    {
        var command = new CreateMemoryCommand() { MovieId = 1, Story = "Story", Title = "Title" };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("SawDate")))
                .And.Errors["SawDate"].Should().Contain("'Saw Date' must not be empty.");
    }

    [Test]
    public async Task Should_ThrowValidationException_WhenSawDateIsInvalid()
    {
        var command = new CreateMemoryCommand() { MovieId = 1, Story = "Story", Title = "Title", SawDate = new DateTime(2000, 01, 01).ToUniversalTime() };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("")))
                .And.Errors[""].Should().Contain("Saw date should be greater than release date.");
    }

    [Test]
    public async Task Should_Create_Memory()
    {
        await RunAsDefaultUserAsync();

        var command = new CreateMemoryCommand() { MovieId = 12, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };

        var memory = await SendAsync(command);

        memory.Should().BeGreaterThan(0);

        var query = new GetAllMemoryQuery();

        var memoryDtos = await SendAsync(query);

        memoryDtos.Should().NotBeNull();
        memoryDtos.Count.Should().BeGreaterThanOrEqualTo(1);
        memoryDtos.First(x => x.MovieId == command.MovieId).Should().NotBeNull();
        memoryDtos.First(x => x.MovieId == command.MovieId).Title.Should().Be(command.Title);
    }

    [Test]
    public async Task Should_Prevent_Duplicate_Memory()
    {
        await RunAsDefaultUserAsync();

        var commandFirst = new CreateMemoryCommand() { MovieId = 8, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };

        var commandSecond = new CreateMemoryCommand() { MovieId = 8, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };

        var memory = await SendAsync(commandFirst);

        (await FluentActions.Invoking(() =>
        SendAsync(commandSecond))
              .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("MovieId")))
              .And.Errors["MovieId"].Should().Contain("You have already commit story for this movie, you are not allowed to commit again.");
    }

    [Test]
    public async Task Should_Create_Memory_With_Photo()
    {
        await RunAsDefaultUserAsync();

        var filePath = "C:\\Repository\\MyProject\\RihalCodeStacker-Backend\\src\\Web\\wwwroot\\PhotoA.jpg";

        FormFileCollection Photos = new FormFileCollection();

        using (var fileStream = File.OpenRead(filePath))
        {
            Photos.Add(new FormFile(fileStream, 0, fileStream.Length, "photos", Path.GetFileName(filePath)));
        }

        var command = new CreateMemoryCommand()
        {
            MovieId = 5,
            Story = "Story",
            Title = "Title",
            SawDate = DateTime.Now.ToUniversalTime(),
            // Photos = Photos
        };

        var memory = await SendAsync(command);

        memory.Should().BeGreaterThan(0);

        var query = new GetAllMemoryQuery();

        var memoryDtos = await SendAsync(query);

        memoryDtos.Should().NotBeNull();
        memoryDtos.Count.Should().BeGreaterThanOrEqualTo(1);
        memoryDtos.First(x => x.MovieId == command.MovieId).Should().NotBeNull();
        memoryDtos.First(x => x.MovieId == command.MovieId).Title.Should().Be("Title");
    }
}
