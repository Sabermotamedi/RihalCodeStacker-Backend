namespace Rihal.ReelRise.Domain.Entities;
public class MovieRate : BaseAuditableEntity
{   
    public MovieRate(string? userId)
    {
        if (userId is not null && !string.IsNullOrEmpty(userId))
        {
            UserId = userId;
        }
        else
        {
            throw new ArgumentNullException(nameof(UserId));
        }
    }

    public int MovieId { get; set; }
    public string UserId { get; set; }
    public int Rate { get; set; }
}
