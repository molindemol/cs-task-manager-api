using Microsoft.AspNetCore.Mvc;
using taskManagerApi.Services;

namespace taskManagerApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TaskController: ControllerBase
{
    public TaskController()
    {
        
    }

    [HttpGet]
    public ActionResult<List<Models.Task>> GetAll() => TaskService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Models.Task> Get(int id)
    {
        var task = TaskService.Get(id);

        if(task == null)
            return NotFound();

        return task;
    }

    [HttpPost]
    public IActionResult Create(Models.Task task)
    {            
        TaskService.Add(task);
        return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Models.UpdateTaskDto taskUpdates)
    {
        var existingtask = TaskService.Get(id);
        if(existingtask is null)
            return NotFound();
    
        TaskService.Update(existingtask, taskUpdates);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = TaskService.Get(id);
    
        if (task is null)
            return NotFound();
        
        TaskService.Delete(id);
    
        return NoContent();
    }
}