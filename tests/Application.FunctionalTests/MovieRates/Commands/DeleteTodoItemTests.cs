using Rihal.ReelRise.Application.TodoItems.Commands.CreateTodoItem;
using Rihal.ReelRise.Application.TodoItems.Commands.DeleteTodoItem;
using Rihal.ReelRise.Application.TodoLists.Commands.CreateTodoList;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.FunctionalTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteTodoItemCommand(itemId));

        var item = await FindAsync<FilmCrew>(itemId);

        item.Should().BeNull();
    }
}
