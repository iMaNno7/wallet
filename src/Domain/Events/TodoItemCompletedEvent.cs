namespace CleanArchitecture.Domain.Events;

public class TodoItemCompletedEvent : DomainEvent
{
    public TodoItemCompletedEvent(Transaction item)
    {
        Item = item;
    }

    public Transaction Item { get; }
}
