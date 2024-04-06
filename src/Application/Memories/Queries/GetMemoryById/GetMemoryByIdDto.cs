using System.Collections.Generic;
using System.Drawing;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class GetMemoryByIdDto
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public string? MovieName { get; set; }

    public string? Title { get; set; }

    public string? Story { get; set; }

    public List<GetMemoryByIdPhotoDto>? MemoryPhotos { get; set; }    

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Memory, GetMemoryByIdDto>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie != null ? src.Movie.Name : ""));
        }
    }
}


public class GetMemoryByIdPhotoDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Extension { get; set; }

    public decimal Size { get; set; }

    public DateTime? Created { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MemoryPhoto, GetMemoryByIdPhotoDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.DateTime));

        }
    }
}
