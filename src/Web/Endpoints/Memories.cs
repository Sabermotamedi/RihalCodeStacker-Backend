using Microsoft.AspNetCore.Mvc;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Web.Endpoints;

public class Memories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateMemory);
    }

    public Task<int> CreateMemory(ISender sender, [FromForm] CreateMemoryCommand command, [FromForm] List<IFormFile> photos)
    {
        return sender.Send(command);
    }

    public Task<List<GetAllMemoryDto>> GetAllMemory(ISender sender)
    {
        return sender.Send(new GetAllMemoryQuery());
    }
}
