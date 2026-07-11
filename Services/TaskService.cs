using Microsoft.EntityFrameworkCore;
using taskManagerApi.Models;

namespace taskManagerApi.Services;
public class TaskService(TaskDb db)
{
    private readonly TaskDb _db = db;

    public List<TaskDto> GetAll()
    {
        var tasks = _db.Tasks.Include(t => t.Column).ToList();
        return tasks.Select(t => MapTaskToDto(t)).ToList();
    }
    
    public TaskDto? Get(int id)
    {
        var task = _db.Tasks.Include(t => t.Column).FirstOrDefault(p => p.Id == id);
        return task == null ? null : MapTaskToDto(task);
    }

    public void Add(Models.Task task)
    {
        _db.Tasks.Add(task);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
        if(task is null)
            return;

        _db.Tasks.Remove(task);
        _db.SaveChanges();
    }

    public void Update(int id, UpdateTaskDto updates)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
            return;

        if (updates.Title != null)
            task.Title = updates.Title;

        if (updates.Description != null)
            task.Description = updates.Description;

        if (updates.Status != null)
            task.Status = updates.Status;

        if (updates.Position.HasValue)
            task.Position = updates.Position.Value;

        if (updates.ColumnId.HasValue)
            task.ColumnId = updates.ColumnId.Value;

        _db.SaveChanges();
    }

    private TaskDto MapTaskToDto(Models.Task task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Position = task.Position,
            ColumnId = task.ColumnId
        };
    }
}