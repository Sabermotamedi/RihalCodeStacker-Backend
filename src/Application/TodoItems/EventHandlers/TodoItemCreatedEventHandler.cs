﻿using Rihal.ReelRise.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Rihal.ReelRise.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rihal.ReelRise Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
