using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Wallet.Commands.DeleteTodoItem;

public class DeleteWalletCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteWlletCommandHandler : IRequestHandler<DeleteWalletCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteWlletCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Wallet
            .FirstOrDefaultAsync(a=>a.Id==request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        _context.Wallet.Remove(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
