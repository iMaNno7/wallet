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

public class GetAllWalletQuery : IRequest<List<GetAllWalletVm>>
{
    public string UserId { get; set; }
}
public class GetAllWalletQueryHandler : IRequestHandler<GetAllWalletQuery, List<GetAllWalletVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllWalletQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetAllWalletVm>> Handle(GetAllWalletQuery request, CancellationToken cancellationToken)
    {
        return _context.Wallet.
             AsNoTracking()
            .Where(a => a.IdentityUser == request.UserId)
            .ProjectTo<GetAllWalletVm>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
