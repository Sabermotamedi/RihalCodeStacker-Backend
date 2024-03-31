namespace Rihal.ReelRise.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItemDeletedEvent(FilmCrew item)
    {
        Item = item;
    }

    public FilmCrew Item { get; }
}
