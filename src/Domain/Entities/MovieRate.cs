namespace Rihal.ReelRise.Domain.Entities;
public class MovieRate : BaseAuditableEntity
{   
    public MovieRate()
    {      
    }

    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    public string? UserId { get; set; }
    public int Rate { get; set; }
}
