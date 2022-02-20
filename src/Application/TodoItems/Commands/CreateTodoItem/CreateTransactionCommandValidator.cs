using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionCommandValidator(IApplicationDbContext context)
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .WithMessage("عنوان بیشتز از حد مجاز است")
            .NotEmpty();
        RuleFor(v => v.UserId)
            .MustAsync(CheckWallet)
            .WithMessage("کیف پول موجود نیست")
            .NotEmpty()   ;
        _context = context;
    }
    private Task<bool> CheckWallet(string id,CancellationToken cancellationToken) =>
        _context.Wallet.AnyAsync(a => a.IdentityUser == id, cancellationToken);
}
