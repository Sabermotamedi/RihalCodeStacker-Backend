using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class GetAllMemoryDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string? MovieName { get; set; }
    public string? Title { get; set; }
    public List<string>? Photos { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Memory, GetAllMemoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie != null ? src.Movie.Name : ""))  
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.MemoryPhotos != null? src.MemoryPhotos.Select(x=>x.Name).ToList():null))     
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        }
    }
}
