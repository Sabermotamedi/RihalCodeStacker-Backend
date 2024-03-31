﻿using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.TodoLists.Queries.GetTodos;

public class TodoItemDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public int Priority { get; init; }

    public string? Note { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<FilmCrew, TodoItemDto>().ForMember(d => d.Priority, 
                opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
