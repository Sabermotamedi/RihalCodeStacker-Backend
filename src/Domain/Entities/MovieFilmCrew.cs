using System.ComponentModel.DataAnnotations;

namespace Rihal.ReelRise.Domain.Entities;
public class MovieFilmCrew
{
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    public int FilmCrewId { get; set; }
    public FilmCrew? FilmCrew { get; set; }

    public FilmCrewType CrewType { get; set; }

}
