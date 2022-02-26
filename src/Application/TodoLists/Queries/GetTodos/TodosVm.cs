namespace CleanArchitecture.Application.TodoLists.Queries.GetTodos;

public class TodosVm
{
    public IList<PriorityLevelDto> PriorityLevels { get; set; } = new List<PriorityLevelDto>();
    
    public  List<TodoListDto> Lists { get; set; } = new List<TodoListDto>();
}
