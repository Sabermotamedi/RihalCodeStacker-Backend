using FluentValidation.TestHelper;
using Rihal.ReelRise.Application.Common.Exceptions;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.TodoLists.Commands.CreateTodoList;

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

        var command = new CreateMemoryCommand() { MovieId = 1, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };

        var memory = await SendAsync(command);

        memory.Should().BeGreaterThan(0);

    }

    [Test]
    public async Task Should_Prevent_Duplicate_Memory()
    {
        await RunAsDefaultUserAsync();

        var commandFirst = new CreateMemoryCommand() { MovieId = 1, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };

        var commandSecond = new CreateMemoryCommand() { MovieId = 1, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };

        var memory = await SendAsync(commandFirst);

        (await FluentActions.Invoking(() =>
        SendAsync(commandSecond))
              .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("MovieId")))
              .And.Errors["MovieId"].Should().Contain("You have already commit story for this movie, you are not allowed to commit again.");
    }
}
