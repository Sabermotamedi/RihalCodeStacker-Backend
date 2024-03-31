namespace Rihal.ReelRise.Domain.Entities;

public class FilmCrew : BaseAuditableEntity
{
    public FilmCrew Create(string name)
    {
        return new FilmCrew() { Name = name };
    }
    public string? Name { get; set; }

    public ICollection<MovieFilmCrew> MovieFilmCrews { get; set; } = new List<MovieFilmCrew>();
}


