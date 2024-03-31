namespace Rihal.ReelRise.Domain.Entities;

public class Movie : BaseAuditableEntity
{
    public Movie()
    {
        Created = DateTime.Now;
    }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Budget { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public ICollection<MovieFilmCrew> MovieFilmCrews { get; set; } = new List<MovieFilmCrew>();
}
