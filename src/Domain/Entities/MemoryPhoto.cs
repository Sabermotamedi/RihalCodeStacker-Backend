using System.Drawing;

namespace Rihal.ReelRise.Domain.Entities;
public class MemoryPhoto : BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? UniqueName { get; set; }

    public string? Extension { get; set; }

    public decimal Size { get; set; }
    
    public int MemoryId { get; set; }
    public Memory? Memory { get; set; }
}
