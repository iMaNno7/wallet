namespace CleanArchitecture.Domain.Events;

public class TodoItemDeletedEvent : DomainEvent
{
    public TodoItemDeletedEvent(Transaction item)
    {
        Item = item;
    }

    public Transaction Item { get; }
}
