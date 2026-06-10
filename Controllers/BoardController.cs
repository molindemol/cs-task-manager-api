using Microsoft.AspNetCore.Mvc;
using taskManagerApi.Models;
using taskManagerApi.Services;

namespace taskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardController : ControllerBase
{
    private readonly BoardService _boardService;

    public BoardController(BoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    public ActionResult<List<BoardDto>> GetAll() => _boardService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<BoardDto> Get(int id)
    {
        var board = _boardService.Get(id);

        if(board == null)
            return NotFound();

        return board;
    }

    [HttpPost]
    public IActionResult Create(CreateBoardDto createBoardDto)
    {
        var board = new Board
        {
            Name = createBoardDto.Name,
            Description = createBoardDto.Description
        };
        _boardService.Add(board);
        return CreatedAtAction(nameof(Get), new { id = board.Id }, _boardService.Get(board.Id));
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateBoardDto boardUpdates)
    {
        var existingBoard = _boardService.Get(id);
        if(existingBoard is null)
            return NotFound();

        var board = new Board { Id = id };
        _boardService.Update(board, boardUpdates);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var board = _boardService.Get(id);
    
        if (board is null)
            return NotFound();
        
        _boardService.Delete(id);
    
        return NoContent();
    }
}
