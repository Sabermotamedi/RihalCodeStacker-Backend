using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
public class ExtractStoryLinkByIdDto
{
    public int Id { get; set; }

    public List<string>? Links { get; set; }
}
