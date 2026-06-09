using Microsoft.EntityFrameworkCore;
using taskManagerApi.Models;

namespace taskManagerApi.Services;
public class TaskService(TaskDb db)
{
    private readonly TaskDb _db = db;

    public List<Models.Task> GetAll() => [.. _db.Tasks];
    
    public Models.Task? Get(int id) => _db.Tasks.FirstOrDefault(p => p.Id == id);

    public void Add(Models.Task task)
    {
        _db.Tasks.Add(task);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var task = Get(id);
        if(task is null)
            return;

        _db.Tasks.Remove(task);
        _db.SaveChanges();
    }

    public void Update(Models.Task task, UpdateTaskDto updates)
    {
        if (updates.Title != null)
            task.Title = updates.Title;

        if (updates.Description != null)
            task.Description = updates.Description;

        if (updates.Status != null)
            task.Status = updates.Status;

        _db.Tasks.Update(task);
        _db.SaveChanges();
    }
}