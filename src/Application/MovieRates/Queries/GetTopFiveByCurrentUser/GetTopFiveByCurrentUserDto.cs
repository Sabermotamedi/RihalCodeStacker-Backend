using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class GetTopFiveByCurrentUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Rate { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MovieRate, GetTopFiveByCurrentUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Movie != null ? src.Movie.Name : ""));
        }
    }
}
