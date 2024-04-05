namespace Rihal.ReelRise.Domain.Entities;
public class MemoryPhoto : BaseAuditableEntity
{
    public string? Name { get; set; }

    public int MemoryId { get; set; }
    public Memory? Memory { get; set; }
}
