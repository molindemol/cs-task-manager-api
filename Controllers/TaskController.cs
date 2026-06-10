using Microsoft.AspNetCore.Mvc;
using taskManagerApi.Services;
using taskManagerApi.Models;

namespace taskManagerApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TaskController: ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<List<TaskDto>> GetAll() => _taskService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<TaskDto> Get(int id)
    {
        var task = _taskService.Get(id);

        if(task == null)
            return NotFound();

        return task;
    }

    [HttpPost]
    public IActionResult Create(Models.Task task)
    {            
        _taskService.Add(task);
        return CreatedAtAction(nameof(Get), new { id = task.Id }, _taskService.Get(task.Id));
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateTaskDto taskUpdates)
    {
        var existingtask = _taskService.Get(id);
        if(existingtask is null)
            return NotFound();

        var task = new Models.Task { Id = id };
        _taskService.Update(task, taskUpdates);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _taskService.Get(id);
    
        if (task is null)
            return NotFound();
        
        _taskService.Delete(id);
    
        return NoContent();
    }
}