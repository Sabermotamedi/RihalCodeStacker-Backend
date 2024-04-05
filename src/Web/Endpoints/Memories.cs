using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;

namespace Rihal.ReelRise.Web.Endpoints;

public class Memories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateMemory);
    }

    public Task<int> CreateMemory(ISender sender, CreateMemoryCommand command)
    {
        return sender.Send(command);
    }

}
