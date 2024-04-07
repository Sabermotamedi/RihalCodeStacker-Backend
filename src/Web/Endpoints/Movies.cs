using Microsoft.AspNetCore.Mvc;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
using Rihal.ReelRise.Application.Movies.Queries.SearchMovie;

namespace Rihal.ReelRise.Web.Endpoints;

public class Movies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllMovieWithRate)
            .MapGet(GetMovieById, "{id}")
            .MapGet(SearchMovie, "search")
            .MapGet(GetMovieByUnscrambleName, "UnscrambleName/{name}");
    }

    public Task<List<GetAllMovieWithRateDto>> GetAllMovieWithRate(ISender sender)
    {
        return sender.Send(new GetAllMovieWithRateQuery());
    }

    public Task<MovieDto> GetMovieById(ISender sender, int id)
    {
        return sender.Send(new GetMovieByIdQuery { Id = id });
    }

    public Task<List<SearchMovieDto>> SearchMovie(ISender sender, [FromQuery] string param)
    {
        return sender.Send(new SearchMovieQuery { SearchValue = param });
    }

    public Task<GetMovieByUnscrambleNameDto> GetMovieByUnscrambleName(ISender sender, string name)
    {
        return sender.Send(new GetMovieByUnscrambleNameQuery() { Name = name });
    }
}
