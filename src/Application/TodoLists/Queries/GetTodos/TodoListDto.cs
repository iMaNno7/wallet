using AutoMapper;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.TodoLists.Queries.GetTodos;

public class TodoListDto : IMapFrom<Category>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime CreateDateTime { get; set; }
    public string Created { get; set; }
    public decimal Total { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
    public IList<TodoItemDto> Items { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, TodoListDto>()
            .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToShamsi())).ReverseMap();
        profile.CreateMap<Category, TodoListDto>()
            .ForMember(d => d.CreateDateTime, opt => opt.MapFrom(s => s.Created)).ReverseMap();
    }
}
