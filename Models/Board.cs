namespace taskManagerApi.Models;

public class Board
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Column> Columns { get; set; } = new List<Column>();
}
