using Microsoft.EntityFrameworkCore;
using taskManagerApi.Models;

namespace taskManagerApi.Services;

public class ColumnService(TaskDb db)
{
    private readonly TaskDb _db = db;

    public List<ColumnDto> GetAll()
    {
        var columns = _db.Columns.Include(c => c.Tasks).ToList();
        return columns.Select(c => MapColumnToDto(c)).ToList();
    }
    
    public ColumnDto? Get(int id)
    {
        var column = _db.Columns.Include(c => c.Tasks).FirstOrDefault(c => c.Id == id);
        return column == null ? null : MapColumnToDto(column);
    }

    public List<ColumnDto> GetByBoardId(int boardId)
    {
        var columns = _db.Columns.Where(c => c.BoardId == boardId).Include(c => c.Tasks).OrderBy(c => c.Position).ToList();
        return columns.Select(c => MapColumnToDto(c)).ToList();
    }

    public void Add(Column column)
    {
        _db.Columns.Add(column);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var column = _db.Columns.FirstOrDefault(c => c.Id == id);
        if(column is null)
            return;

        _db.Columns.Remove(column);
        _db.SaveChanges();
    }

    public void Update(Column column, UpdateColumnDto updates)
    {
        if (updates.Name != null)
            column.Name = updates.Name;

        if (updates.Position.HasValue)
            column.Position = updates.Position.Value;

        _db.Columns.Update(column);
        _db.SaveChanges();
    }

    private ColumnDto MapColumnToDto(Column column)
    {
        return new ColumnDto
        {
            Id = column.Id,
            Name = column.Name,
            Position = column.Position,
            BoardId = column.BoardId,
            Tasks = column.Tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                ColumnId = t.ColumnId
            }).ToList()
        };
    }
}

public class UpdateColumnDto
{
    public string? Name { get; set; }
    public int? Position { get; set; }
}
