using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateWalletCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public bool IscActive { get; set; }
}

public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateWalletCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Wallet
            .FirstOrDefaultAsync(a=>a.Id==request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        entity.Title = request.Title;
        entity.IscActive = request.IscActive;
        entity.DomainEvents.Add(new WalletUpdatedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
