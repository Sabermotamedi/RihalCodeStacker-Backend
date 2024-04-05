using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;

public record CreateMemoryCommand : IRequest<int>
{
    public int MovieId { get; init; }

    public string? Title { get; set; }

    public string? Story { get; set; }

    public List<string>? Photos { get; set; }

    public DateTime SawDate { get; set; }
}

public class CreateMemoryCommandHandler : IRequestHandler<CreateMemoryCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateMemoryCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }
    //What if our user add duplicate rate for a movie? //Bug
    public async Task<int> Handle(CreateMemoryCommand request, CancellationToken cancellationToken)
    {
        Memory entity = new Memory()
        {
            UserId = _user.Id,
            MovieId = request.MovieId,
            Title = request.Title,
            Story = request.Story,
            SawDate = request.SawDate
        };

        _context.Memories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
