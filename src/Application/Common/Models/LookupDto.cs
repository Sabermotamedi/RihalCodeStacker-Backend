using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Movie, LookupDto>();
            CreateMap<FilmCrew, LookupDto>();
        }
    }
}
