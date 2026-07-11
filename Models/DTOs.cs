namespace taskManagerApi.Models;

// Board DTOs
public class BoardDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ColumnDto> Columns { get; set; } = new();
}

public class CreateBoardDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

// Column DTOs
public class ColumnDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Position { get; set; }
    public int BoardId { get; set; }
    public List<TaskDto> Tasks { get; set; } = new();
}

public class CreateColumnDto
{
    public string? Name { get; set; }
    public int Position { get; set; }
    public int BoardId { get; set; }
}

// Task DTO
public class TaskDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int Position { get; set; }
    public int? ColumnId { get; set; }
}
