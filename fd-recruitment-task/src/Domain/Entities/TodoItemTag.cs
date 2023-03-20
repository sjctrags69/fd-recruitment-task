namespace Todo_App.Domain.Entities;

public class TodoItemTag : BaseAuditableEntity
{
  public int ItemId {get; set;}
  public string? Tag { get; set; }
  public TodoItem Item { get; set; } = null!;
}