using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTransactionCommand : IRequest<int>
{
    public int ListId { get; set; }
    public string UserId { get; set; }

    public string? Title { get; set; }
    public string? Note { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }

}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Transaction
        {
            CategoryId = request.ListId,
            Title = request.Title,
            TransactionType = request.TransactionType,
            Amount = new Amount(request.Amount),
            Done = false,
            Note = request.Note
        };

        var wallet = await _context.Wallet
            .OrderBy(a=>a.Id)
            .LastOrDefaultAsync(a => a.IdentityUser == request.UserId && a.IscActive);

        wallet.AddTransactiom(entity);


        _context.Wallet.Update(wallet);

        entity.DomainEvents.Add(new TransactionCreatedEvent(entity));
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            throw;
        }

        return entity.Id;
    }
}
