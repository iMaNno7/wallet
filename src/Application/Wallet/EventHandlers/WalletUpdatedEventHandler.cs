using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Wallet.EventHandlers;

public class WalletUpdatedEventHandler : INotificationHandler<DomainEventNotification<WalletUpdatedEvent>>
{
    private readonly IApplicationDbContext _context;

    public WalletUpdatedEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DomainEventNotification<WalletUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var wallets = await _context.Wallet
            .Where(a => a.Id != domainEvent.Item.Id && a.IscActive)
            .ToListAsync(cancellationToken);

        wallets.ForEach(a => a.IscActive = false);
        _context.Wallet.UpdateRange(wallets);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
