using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;
using Rihal.ReelRise.Application.Movies.Queries;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Web.Endpoints;

public class MovieRates : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTopFiveByCurrentUser, "GetTopFiveByCurrentUser")
            .MapPost(CreateMovieRate);
    }
      
    public Task<int> CreateMovieRate(ISender sender, CreateMovieRateCommand command)
    {
        return sender.Send(command);
    }

    public async Task<List<GetTopFiveByCurrentUserDto>> GetTopFiveByCurrentUser(ISender sender)
    {
        return await sender.Send(new GetTopFiveByCurrentUserQuery());
    }

}
