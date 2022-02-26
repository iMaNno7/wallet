using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Wallet.Queries;

public class GetAllWalletVm:IMapFrom<Domain.Entities.WalletAggregate.Wallet> {
    public Guid Id { get; set; }
    public Balance Balance { get; private set; } = new();
    public decimal Withdrawal { get; set; }
    public decimal Deposit { get; set; }
    public string Title { get; set; }
    public bool IscActive { get; set; }

}

