namespace CleanArchitecture.Domain.Events;

public class TransactionCreatedEvent : DomainEvent
{
    public TransactionCreatedEvent(Transaction item)
    {
        Item = item;
    }

    public Transaction Item { get; }
}
