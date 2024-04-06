using Microsoft.AspNetCore.Mvc;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Rihal.ReelRise.Web.Endpoints;

public class Memories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .DisableAntiforgery()
            .MapGet(GetMemoryById, "{id}")
            .MapGet(GetMemoryPhotoById, "photo/{id}")
            .MapPut(UpdateMemory, "{id}")
            .MapPost(CreateMemory)
            .MapDelete(DeleteMemory, "{id}")
            .MapPost(DeleteOrDeleteMemoryPhoto, "DeleteAndDelete");
    }

    public Task<int> CreateMemory(ISender sender, [FromForm] IFormFileCollection photos, [FromForm] CreateMemoryCommand command)
    {
        return sender.Send(command);
    }

    public Task<List<GetAllMemoryDto>> GetAllMemory(ISender sender)
    {
        return sender.Send(new GetAllMemoryQuery());
    }

    public Task<GetMemoryByIdDto> GetMemoryById(ISender sender, int id)
    {
        return sender.Send(new GetMemoryByIdQuery() { Id = id });
    }

    public async Task<IResult> GetMemoryPhotoById(ISender sender, int id)
    {
        var stream = await sender.Send(new GetPhotoByIdQuery() { Id = id });

        if (stream == null || !stream.CanRead)
        {
            return Results.NotFound();
        }
        return Results.File(stream, "image/jpeg");
    }

    public async Task<IResult> UpdateMemory(ISender sender, int id, UpdateMemoryCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteMemory(ISender sender, int id)
    {
        await sender.Send(new DeleteMemoryCommand() { MemoryId = id });
        return Results.NoContent();
    }

    public async Task<IResult> DeleteOrDeleteMemoryPhoto(ISender sender,
                                                        [FromForm] IFormFileCollection photos,
                                                        [FromForm] UpdateOrDeleteMemoryPhotoCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
