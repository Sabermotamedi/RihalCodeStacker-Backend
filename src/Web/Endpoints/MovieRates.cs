using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;

namespace Rihal.ReelRise.Web.Endpoints;

public class MovieRates : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateMovieRate);
            //.MapGet(GetTodoItemsWithPagination)
            //.MapPut(UpdateTodoItem, "{id}")
            //.MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
            //.MapDelete(DeleteTodoItem, "{id}");
    }

    //public Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    //{
    //    return sender.Send(query);
    //}

    public Task<int> CreateMovieRate(ISender sender, CreateMovieRateCommand command)
    {      
        return sender.Send(command);
    }

    //public async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
    //{
    //    if (id != command.Id) return Results.BadRequest();
    //    await sender.Send(command);
    //    return Results.NoContent();
    //}

    //public async Task<IResult> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
    //{
    //    if (id != command.Id) return Results.BadRequest();
    //    await sender.Send(command);
    //    return Results.NoContent();
    //}

    //public async Task<IResult> DeleteTodoItem(ISender sender, int id)
    //{
    //    await sender.Send(new DeleteTodoItemCommand(id));
    //    return Results.NoContent();
    //}
}
