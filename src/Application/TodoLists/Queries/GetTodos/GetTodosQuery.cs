using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoLists.Queries.GetTodos;

public class GetTodosQuery : IRequest<TodosVm>
{
    public string? UserId { get; set; }
}

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var vms = new TodosVm();

        vms.PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList();
        var walletID =await _context.Wallet.AsQueryable().
            Where(a => a.IscActive == true && a.IdentityUser == request.UserId)
            .Select(a => a.Id).FirstOrDefaultAsync();
        vms.Lists = await _context.Category
            .AsNoTracking()
            .Where(a => a.Items.Any(a => a.WalletId==walletID))
            .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Title)
            .ToListAsync(cancellationToken);

        return vms;

    }
}
