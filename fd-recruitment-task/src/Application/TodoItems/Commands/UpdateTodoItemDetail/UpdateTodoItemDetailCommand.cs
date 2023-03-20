using MediatR;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }

    public string? Colour { get; init; }

    public string? Tag { get; init;}

    public List<TodoItemTag>? ItemTags {get; set;}


    public Dictionary<string, string>? ItemDelTags {get; set;}

}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

             
       

        if (! (request.Tag == "") || request.Tag == null)
        {
             TodoItemTag item = new TodoItemTag(){
            ItemId= request.Id,
            Tag = request.Tag
        };
         _context.ItemTags.Add(item);
         entity.ItemTags.Append(item);
        }
       

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;
        entity.Colour = (Colour) (request.Colour ?? "#FFFFFF");
       
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
