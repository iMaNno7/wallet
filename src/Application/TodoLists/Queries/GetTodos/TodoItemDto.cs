using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.TodoLists.Queries.GetTodos;

public class TodoItemDto : IMapFrom<Transaction>
{
    public int Id { get; set; }

    public int ListId { get; set; }


    public bool Done { get; set; }

    public int Priority { get; set; }
    public Guid WalletId { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Transaction, TodoItemDto>()
            .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority))
            .ForMember(d => d.ListId, opt => opt.MapFrom(s => s.CategoryId))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount.Value)).ReverseMap();
    }
}
