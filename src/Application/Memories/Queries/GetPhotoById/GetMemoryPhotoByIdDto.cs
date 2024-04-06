using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class GetMemoryPhotoByIdDto
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public string? MovieName { get; set; }

    public string? Title { get; set; }

    public string? Story { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Memory, GetMemoryPhotoByIdDto>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie != null ? src.Movie.Name : ""));
        }
    }
}
