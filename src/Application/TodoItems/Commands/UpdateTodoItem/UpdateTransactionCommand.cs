using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateTransactionCommand : IRequest
{
    public int Id { get; set; }
    public int ListId { get; set; }
    public string UserId { get; set; }

    public string? Title { get; set; }
    public string? Note { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
}

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Transaction
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        var wallet = await _context.Wallet
            .FirstOrDefaultAsync(a => a.Id == entity.WalletId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        entity.Title = request.Title;
        entity.CategoryId = request.ListId;
        entity.Note = request.Note;
        if (entity.TransactionType != request.TransactionType && entity.Amount.Value != request.Amount)
        wallet.ChangeTransactionTypeAndAmount(entity.Amount, new(request.Amount), request.TransactionType);
        else if (entity.TransactionType != request.TransactionType)
            wallet.ChangeTransactionType(entity.Amount, request.TransactionType);
        else if (entity.Amount.Value != request.Amount)
            wallet.updateBalance(entity.Amount, new(request.Amount), request.TransactionType);

        entity.TransactionType = request.TransactionType;
        entity.Amount = new(request.Amount);
        _context.Wallet.Update(wallet);
        _context.Transaction.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
