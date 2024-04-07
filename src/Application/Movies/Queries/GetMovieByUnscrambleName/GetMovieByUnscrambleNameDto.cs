using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class GetMovieByUnscrambleNameDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Movie, GetMovieByUnscrambleNameDto>();
        }
    }
}
