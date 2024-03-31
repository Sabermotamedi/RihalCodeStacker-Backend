namespace Rihal.ReelRise.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(FilmCrew item)
    {
        Item = item;
    }

    public FilmCrew Item { get; }
}
