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
    public DateTime? CreateDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }

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
        var walletID = await _context.Wallet.AsQueryable().
            Where(a => a.IscActive == true && a.IdentityUser == request.UserId)
            .Select(a => a.Id).FirstOrDefaultAsync();
        vms.Lists = await _context.Category
            .AsNoTracking()
            .Where(a => a.Items.Any(a => a.WalletId == walletID))
            .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Title)
            .ToListAsync(cancellationToken);
        if (request.CreateDateTime.HasValue)
            vms.Lists.ForEach(x => x.Items = x.Items.Where(s => s.Datetime >= request.CreateDateTime.Value.AddDays(1)).ToList());
        if (request.EndDateTime.HasValue)
            vms.Lists.ForEach(x => x.Items= x.Items.Where(s => s.Datetime <= request.EndDateTime.Value.AddDays(1)).ToList());


        vms.Lists.ForEach(a => a.Total = a.Items.Where(v => v.TransactionType == TransactionType.Deposit).Sum(r => r.Amount) - a.Items.Where(v => v.TransactionType == TransactionType.Withdrawal).Sum(r => r.Amount));

        return vms;

    }
}
