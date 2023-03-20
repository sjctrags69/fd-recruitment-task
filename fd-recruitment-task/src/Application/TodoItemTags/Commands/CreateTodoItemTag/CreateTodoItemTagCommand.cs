using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Events;

namespace Todo_App.Application.TodoItemTags.CreateTodoItemTag;

public record CreateTodoItemTagCommand : IRequest<int>
{
    public int ItemId { get; init; }

    public string? Tag { get; init; }


}

public class CreateTodoItemTagCommandHandler : IRequestHandler<CreateTodoItemTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItemTag
        {
            ItemId = request.ItemId,
            Tag = request.Tag
        };

        entity.AddDomainEvent(new TodoItemTagCreatedEvent(entity));

        _context.ItemTags.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
