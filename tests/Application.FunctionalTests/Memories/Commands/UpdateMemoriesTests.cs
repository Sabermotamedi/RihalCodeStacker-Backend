using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Exceptions;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.TodoLists.Commands.CreateTodoList;

namespace Rihal.ReelRise.Application.FunctionalTests.MovieRates.Commands;
using static Testing;

public class UpdateMemoriesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdateMemoryCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task Should_ThrowValidationException_WhenTitleIsNull()
    {
        var command = new UpdateMemoryCommand() { MemoryId = 1, Story = "Story" };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("'Title' must not be empty.");
    }


    [Test]
    public async Task Should_ThrowValidationException_WhenStoryIsNull()
    {
        var command = new UpdateMemoryCommand() { MemoryId = 1, Title = "Title" };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Story")))
                .And.Errors["Story"].Should().Contain("'Story' must not be empty.");
    }

    [Test]
    public async Task Should_Update_Memory()
    {
        await RunAsDefaultUserAsync();

        // Create
        var command = new CreateMemoryCommand() { MovieId = 1, Story = "Story", Title = "Title", SawDate = DateTime.Now.ToUniversalTime() };
        var memory = await SendAsync(command);

        memory.Should().BeGreaterThan(0);

        // Get All
        var query = new GetAllMemoryQuery();
        var memoryDtos = await SendAsync(query);

        memoryDtos.Should().NotBeNull();
        memoryDtos.Should().HaveCount(1);
        memoryDtos[0].Should().NotBeNull();
        memoryDtos[0].Title.Should().Be("Title");
        memoryDtos[0].MovieName.Should().Be("Mad Max");

        // Update
        var updateCommand = new UpdateMemoryCommand() { MemoryId = memoryDtos[0].Id, Title = "Title Updated", Story = "Story Updated" };

        await SendAsync(updateCommand);
        
        // Gte By Id
        var getQuery = new GetMemoryByIdQuery() { Id = memoryDtos[0].Id };
        var memoryDtosUpdated = await SendAsync(getQuery);

        memoryDtosUpdated.Should().NotBeNull();
        memoryDtosUpdated.Title.Should().Be("Title Updated");
        memoryDtosUpdated.Story.Should().Be("Story Updated");
    }
}
