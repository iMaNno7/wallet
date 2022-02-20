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

    public  Task<GetAllWalletVm> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        return _context.Wallet.
             AsNoTracking()
            .Where(a => a.IdentityUser == request.UserId && a.IscActive)
            .ProjectTo<GetAllWalletVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
