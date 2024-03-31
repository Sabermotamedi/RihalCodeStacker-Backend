using Rihal.ReelRise.Application.TodoLists.Queries.GetTodos;
using Rihal.ReelRise.Domain.Entities;
using Rihal.ReelRise.Domain.ValueObjects;

namespace Rihal.ReelRise.Application.FunctionalTests.TodoLists.Queries;

using static Testing;

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Movie
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                    {
                        new FilmCrew { Title = "Apples", Done = true },
                        new FilmCrew { Title = "Milk", Done = true },
                        new FilmCrew { Title = "Bread", Done = true },
                        new FilmCrew { Title = "Toilet paper" },
                        new FilmCrew { Title = "Pasta" },
                        new FilmCrew { Title = "Tissues" },
                        new FilmCrew { Title = "Tuna" }
                    }
        });

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
