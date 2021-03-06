using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
    }

    //public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    //{
    //    //return await _context.Category
    //    //    .AllAsync(l => l.Title != title, cancellationToken);
    //}
}
