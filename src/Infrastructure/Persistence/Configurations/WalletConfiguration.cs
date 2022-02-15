using CleanArchitecture.Domain.Entities.WalletAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.Ignore(e => e.DomainEvents);
        builder.OwnsOne(x => x.Balance)
               .Property(p => p.Value)
               .IsRequired();
    }
}
