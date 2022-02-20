using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;

public class DeleteTodoItemCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Transaction
            .FindAsync(new object[] { request.Id }, cancellationToken);

        var wallet = await _context.Wallet
           .FirstOrDefaultAsync(a => a.Id == entity.WalletId);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }
        wallet.RefactorTransaction(entity.Amount, entity.TransactionType);

        _context.Transaction.Remove(entity);
        _context.Wallet.Update(wallet);

        entity.DomainEvents.Add(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
