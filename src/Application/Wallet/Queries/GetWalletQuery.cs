using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Wallet.Queries;

public class GetWalletQuery : IRequest<GetAllWalletVm>
{
    public string UserId { get; set; }
}
public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, GetAllWalletVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWalletQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllWalletVm> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var query = await _context.Wallet.
             AsNoTracking()
            .Where(a => a.IdentityUser == request.UserId && a.IscActive)
            .ProjectTo<GetAllWalletVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        query.Withdrawal = await _context.Transaction.Where(x => x.WalletId == query.Id &&
          x.TransactionType == Domain.Enums.TransactionType.Withdrawal).SumAsync(x => x.Amount.Value);
        query.Deposit = await _context.Transaction.Where(x => x.WalletId == query.Id &&
           x.TransactionType == Domain.Enums.TransactionType.Deposit).SumAsync(x => x.Amount.Value);
        return query;
        }
}
