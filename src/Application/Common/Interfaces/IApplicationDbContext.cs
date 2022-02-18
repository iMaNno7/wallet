using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Category { get; }

    DbSet<Transaction> Transaction { get; }
    DbSet<Domain.Entities.WalletAggregate.Wallet> Wallet { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
