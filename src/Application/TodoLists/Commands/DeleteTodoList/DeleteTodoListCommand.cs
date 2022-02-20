using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;

public class DeleteTodoListCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Category.Include(s => s.Items)
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);


        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }
        foreach (var item in entity.Items)
        {
            var wallet = await _context.Wallet
                 .FirstOrDefaultAsync(a => a.Id == item.WalletId);
            wallet.RefactorTransaction(item.Amount, item.TransactionType);
            _context.Wallet.Update(wallet);
        }
        _context.Category.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
