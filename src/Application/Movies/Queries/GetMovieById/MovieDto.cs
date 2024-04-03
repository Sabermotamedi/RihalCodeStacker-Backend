using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class MovieDto
{
    public MovieDto()
    {
        MainCast = new List<string>();
    }
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public List<string> MainCast { get; set; }
    
    public string? director { get; set; }

    public decimal? Budget { get; set; }

    public string? BudgetEnglishWorld { get; set; }

    public int AverageRate { get; set; }

    public int MyRate { get; set; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Movie, MovieDto>();
        }
    }
}
