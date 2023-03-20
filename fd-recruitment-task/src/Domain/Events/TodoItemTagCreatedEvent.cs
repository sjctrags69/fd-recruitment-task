namespace Todo_App.Domain.Events;
public class TodoItemTagCreatedEvent : BaseEvent
{
    public TodoItemTagCreatedEvent(TodoItemTag item)
    {
        Item = item;
    }

    public TodoItemTag Item { get; }
}