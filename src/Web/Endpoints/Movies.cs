using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Web.Endpoints;

public class Movies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllMovieWithRate);
            //.MapPost(CreateTodoList)
            //.MapPut(UpdateTodoList, "{id}")
            //.MapDelete(DeleteTodoList, "{id}");
    }

    public Task<List<GetAllMovieWithRateDto>> GetAllMovieWithRate(ISender sender)
    {
        return sender.Send(new GetAllMovieWithRateQuery());
    }

    //public Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command)
    //{
    //    return sender.Send(command);
    //}

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
