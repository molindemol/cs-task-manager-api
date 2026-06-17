using Microsoft.EntityFrameworkCore;
using taskManagerApi.Models;

namespace taskManagerApi.Services;

public class BoardService(TaskDb db)
{
    private readonly TaskDb _db = db;

    public List<BoardDto> GetAll()
    {
        var boards = _db.Boards.Include(b => b.Columns).ThenInclude(c => c.Tasks).ToList();
        return boards.Select(b => MapBoardToDto(b)).OrderByDescending(b => b.CreatedAt).ToList();
    }
    
    public BoardDto? Get(int id)
    {
        var board = _db.Boards.Include(b => b.Columns).ThenInclude(c => c.Tasks).FirstOrDefault(b => b.Id == id);
        return board == null ? null : MapBoardToDto(board);
    }

    public void Add(Board board)
    {
        _db.Boards.Add(board);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var board = _db.Boards.FirstOrDefault(b => b.Id == id);
        if(board is null)
            return;

        _db.Boards.Remove(board);
        _db.SaveChanges();
    }

    public void Update(Board board, UpdateBoardDto updates)
    {
        if (updates.Name != null)
            board.Name = updates.Name;

        if (updates.Description != null)
            board.Description = updates.Description;

        _db.Boards.Update(board);
        _db.SaveChanges();
    }

    private BoardDto MapBoardToDto(Board board)
    {
        return new BoardDto
        {
            Id = board.Id,
            Name = board.Name,
            Description = board.Description,
            CreatedAt = board.CreatedAt,
            Columns = board.Columns.OrderBy(c => c.Position).Select(c => new ColumnDto
            {
                Id = c.Id,
                Name = c.Name,
                Position = c.Position,
                BoardId = c.BoardId,
                Tasks = c.Tasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    ColumnId = t.ColumnId
                }).ToList()
            }).ToList()
        };
    }
}

public class UpdateBoardDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
