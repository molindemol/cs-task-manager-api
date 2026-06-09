namespace taskManagerApi.Services;
public static class TaskService
{
    static List<Models.Task> Tasks {get;} = new List<Models.Task>();
    static int nextId = 0;

    public static List<Models.Task> GetAll() => Tasks;
    public static Models.Task? Get(int id) => Tasks.FirstOrDefault(p => p.Id == id);

    public static void Add(Models.Task Task)
    {
        Task.Id = nextId++;
        Tasks.Add(Task);
    }

    public static void Delete(int id)
    {
        var Task = Get(id);
        if(Task is null)
            return;

        Tasks.Remove(Task);
    }

    public static void Update(Models.Task task, Models.UpdateTaskDto updates)
    {
        if (updates.Title != null)
            task.Title = updates.Title;

        if (updates.Description != null)
            task.Description = updates.Description;

        if (updates.Status != null)
            task.Status = updates.Status;


        var index = Tasks.FindIndex(p => p.Id == task.Id);
        if(index == -1)
            return;

        Tasks[index] = task;
    }
}