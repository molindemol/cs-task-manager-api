using Microsoft.AspNetCore.Mvc;
using taskManagerApi.Models;
using taskManagerApi.Services;

namespace taskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ColumnController : ControllerBase
{
    private readonly ColumnService _columnService;

    public ColumnController(ColumnService columnService)
    {
        _columnService = columnService;
    }

    [HttpGet]
    public ActionResult<List<ColumnDto>> GetAll() => _columnService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<ColumnDto> Get(int id)
    {
        var column = _columnService.Get(id);

        if(column == null)
            return NotFound();

        return column;
    }

    [HttpGet("board/{boardId}")]
    public ActionResult<List<ColumnDto>> GetByBoard(int boardId)
    {
        var columns = _columnService.GetByBoardId(boardId);
        return columns;
    }

    [HttpPost]
    public IActionResult Create(CreateColumnDto createColumnDto)
    {
        var column = new Column
        {
            Name = createColumnDto.Name,
            Position = createColumnDto.Position,
            BoardId = createColumnDto.BoardId
        };
        _columnService.Add(column);
        return CreatedAtAction(nameof(Get), new { id = column.Id }, _columnService.Get(column.Id));
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateColumnDto columnUpdates)
    {
        var existingColumn = _columnService.Get(id);
        if(existingColumn is null)
            return NotFound();

        var column = new Column { Id = id };
        _columnService.Update(column, columnUpdates);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var column = _columnService.Get(id);
    
        if (column is null)
            return NotFound();
        
        _columnService.Delete(id);
    
        return NoContent();
    }
}
