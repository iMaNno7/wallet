using CleanArchitecture.Domain.Entities.WalletAggregate;

namespace CleanArchitecture.Domain.Events;

public class WalletUpdatedEvent : DomainEvent
{
    public WalletUpdatedEvent(Wallet item)
    {
        Item = item;
    }

    public Wallet Item { get; }
}
