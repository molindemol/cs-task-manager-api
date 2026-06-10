namespace taskManagerApi.Models;

public class Column
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Position { get; set; }
    
    public int BoardId { get; set; }
    
    public Board? Board { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
