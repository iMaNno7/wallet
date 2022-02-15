﻿using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<Transaction>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
