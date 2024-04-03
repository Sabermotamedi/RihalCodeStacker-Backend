using Microsoft.AspNetCore.Mvc;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Web.Endpoints;

public class Movies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllMovieWithRate)
            .MapGet(GetMovieById, "{id}");


    }

    public Task<List<GetAllMovieWithRateDto>> GetAllMovieWithRate(ISender sender)
    {
        return sender.Send(new GetAllMovieWithRateQuery());
    }

    public Task<MovieDto> GetMovieById(ISender sender, int id)
    {
        return sender.Send(new GetMovieByIdQuery { Id = id });
    }

    //public async Task<IResult> UpdateTodoList(ISender sender, int id, UpdateTodoListCommand command)
    //{
    //    if (id != command.Id) return Results.BadRequest();
    //    await sender.Send(command);
    //    return Results.NoContent();
    //}

    //public async Task<IResult> DeleteTodoList(ISender sender, int id)
    //{
    //    await sender.Send(new DeleteTodoListCommand(id));
    //    return Results.NoContent();
    //}
}
