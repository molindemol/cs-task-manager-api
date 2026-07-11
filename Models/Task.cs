using Microsoft.EntityFrameworkCore;

namespace taskManagerApi.Models;

public class Task
{
    public int Id {get; set;}
    public string? Title {get; set;}
    public string? Description {get; set;}
    public string? Status {get; set;}
    public int Position { get; set; }

    public int? ColumnId { get; set; }

    public Column? Column { get; set; }
}

public class UpdateTaskDto
{
    public string? Title {get; set;}
    public string? Description {get; set;}
    public string? Status {get; set;}
    public int? Position { get; set; }
    public int? ColumnId { get; set; }
}

public class TaskDb : DbContext
{
    public TaskDb(DbContextOptions options) : base(options) { }
    public DbSet<Task> Tasks { get; set; } = null!;
    public DbSet<Column> Columns { get; set; } = null!;
    public DbSet<Board> Boards { get; set; } = null!;
}