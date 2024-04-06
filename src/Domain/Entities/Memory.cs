namespace Rihal.ReelRise.Domain.Entities;
public class Memory : BaseAuditableEntity
{
    public Memory() { }

    public string? Title { get; set; }

    public string? Story { get; set; }

    public DateTime SawDate { get; set; }

    public string? UserId { get; set; }

    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    public ICollection<MemoryPhoto> MemoryPhotos { get; set; } = new List<MemoryPhoto>();
}
