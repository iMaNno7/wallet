using CleanArchitecture.Domain.Entities.WalletAggregate;

namespace CleanArchitecture.Domain.Entities;

public class Transaction : AuditableEntity, IHasDomainEvent
{
    public int CategoryId { get; set; }

    public Amount Amount { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public int WalletId { get; set; }
    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value == true && _done == false)
            {
                DomainEvents.Add(new TodoItemCompletedEvent(this));
            }

            _done = value;
        }
    }

    public Category Category { get; set; } = null!;

    public Wallet Wallet { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
