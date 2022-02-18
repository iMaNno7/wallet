using FluentValidation;

namespace CleanArchitecture.Application.Wallet.Commands.CreateWallet;

public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
{
    public CreateWalletCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty()
            .WithMessage("کاربر کیف پول مشخص نشده است");
    }
}
