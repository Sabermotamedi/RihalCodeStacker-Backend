using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Common.Utilities;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetMovieByIdQuery : IRequest<MovieDto>
{
    public int Id { get; set; }
}

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, MovieDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetMovieByIdHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
                .Include(x => x.MovieFilmCrews).ThenInclude(x => x.FilmCrew)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

        var movieRate = await _context.MovieRates.FirstOrDefaultAsync(x => x.MovieId == request.Id && x.UserId == _user.Id);

        if (movie is not null)
        {
            var result = new MovieDto()
            {
                Id = movie.Id,
                AverageRate = movie.AverageRate,
                Budget = movie.Budget,
                Name = movie.Name,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate.HasValue ? movie.ReleaseDate : null,
                BudgetEnglishWorld = movie.Budget.ToEnglishWords(),

                MyRate = movieRate is not null ? movieRate.Rate : 0,

                MainCast = movie.MovieFilmCrews is not null && movie.MovieFilmCrews.Count > 0 ?
                movie.MovieFilmCrews.Where(x => x.CrewType == Domain.Enums.FilmCrewType.MainCast).Select(x => x.FilmCrew?.Name ?? "").ToList() : new List<string>(),

                director = movie.MovieFilmCrews is not null && movie.MovieFilmCrews.Count > 0 ?
                movie.MovieFilmCrews.FirstOrDefault(x => x.CrewType == Domain.Enums.FilmCrewType.Director)?.FilmCrew?.Name ?? "" : string.Empty,


            };
            return result;
        }
        return new MovieDto();
    }
}
