using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Wallet.Queries;

public class GetAllWalletVm:IMapFrom<Domain.Entities.WalletAggregate.Wallet> {
    public Guid Id { get; set; }
    public Balance Balance { get; private set; } = new();
    public string Title { get; set; }
    public bool IscActive { get; set; }

}

