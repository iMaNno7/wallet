namespace CleanArchitecture.Domain.Events;

public class TodoItemCreatedEvent : DomainEvent
{
    public TodoItemCreatedEvent(Transaction item)
    {
        Item = item;
    }

    public Transaction Item { get; }
}
