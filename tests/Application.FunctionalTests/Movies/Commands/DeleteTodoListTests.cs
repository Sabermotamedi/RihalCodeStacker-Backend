﻿using Rihal.ReelRise.Application.TodoLists.Commands.CreateTodoList;
using Rihal.ReelRise.Application.TodoLists.Commands.DeleteTodoList;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.FunctionalTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<Movie>(listId);

        list.Should().BeNull();
    }
}