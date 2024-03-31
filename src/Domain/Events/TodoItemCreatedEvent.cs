namespace Rihal.ReelRise.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(FilmCrew item)
    {
        Item = item;
    }

    public FilmCrew Item { get; }
}
