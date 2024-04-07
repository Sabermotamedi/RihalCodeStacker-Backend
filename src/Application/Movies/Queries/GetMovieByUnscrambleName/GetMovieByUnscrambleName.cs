using System.Linq;
using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Common.Utilities;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetMovieByUnscrambleNameQuery : IRequest<GetMovieByUnscrambleNameDto>
{
    public string? Name { get; set; }
}

public class GetMovieByUnscrambleNameHandler : IRequestHandler<GetMovieByUnscrambleNameQuery, GetMovieByUnscrambleNameDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ITextService _textService;

    public GetMovieByUnscrambleNameHandler(IApplicationDbContext context, ITextService textService)
    {
        _context = context;
        _textService = textService;
    }

    public async Task<GetMovieByUnscrambleNameDto> Handle(GetMovieByUnscrambleNameQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.Movies
                .AsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Select(x => new MovieName(x.Id, x.Name ?? "")).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.Name) && movies != null && movies.Count > 0)
        {
            var movieDictionary = movies.ToDictionary(x => x.Id, x => x.Name);

            var result = _textService.GuessMovieTitle(request.Name, movieDictionary);

            if (result.Id == 0)
            {
                throw new NotFoundException("movies", "movies");
            }

            var movie = await _context.Movies
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == result.Id);

            if (movie is not null)
            {
                return new GetMovieByUnscrambleNameDto() { Id = movie.Id, Name = movie.Name, Description = movie.Description };
            }
            else
            {
                throw new NotFoundException("movies", "movies");
            }
        }
        else
        {
            throw new NotFoundException("movies", "movies");
        }
    }

    public class MovieName
    {
        public MovieName(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; internal set; }
        public string Name { get; internal set; }
    }
}
