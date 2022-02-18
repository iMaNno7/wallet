using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.WalletAggregate;
using CleanArchitecture.Domain.Events;
using MediatR;
using WalletEntity = CleanArchitecture.Domain.Entities.WalletAggregate.Wallet;

namespace CleanArchitecture.Application.Wallet.Commands.CreateWallet;

public class CreateWalletCommand : IRequest<Guid>
{
    public string UserId { get; set; }
    public bool IsActive { get; set; }=false;
    public string Title { get; set; }
}

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateWalletCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var entity = new WalletEntity(request.UserId,request.Title);
        entity.ChangeStateWallet(request.IsActive);
        
        await _context.Wallet.AddAsync(entity, cancellationToken);
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
