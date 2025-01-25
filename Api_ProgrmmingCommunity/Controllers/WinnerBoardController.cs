using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;

[Route("api/[controller]")]
[ApiController]
public class WinnerBoardController : ControllerBase
    {
    private readonly ProgrammingCommunityContext _context;

    public WinnerBoardController(ProgrammingCommunityContext context)
        {
        _context = context;
        }

    // GET: api/WinnerBoard
    [HttpGet("GetAllWinners")]
    public async Task<ActionResult<IEnumerable<WinnerBoardDTO>>> GetAll()
        {
        var winnerBoards = await _context.WinnerBoards
            .Select(w => new WinnerBoardDTO
                {
                Id = w.Id,
                CompetitionId = w.CompetitionId,
                TeamId = w.TeamId,
                Score = w.Score
                }).ToListAsync();

        return Ok(winnerBoards);
        }

   
    [HttpPost("SaveWinnerResult")]
    public async Task<ActionResult<WinnerBoardDTO>> Post([FromBody] WinnerBoardDTO winnerBoardDto)
        {
        if (winnerBoardDto == null)
            {
            return BadRequest("Invalid data.");
            }

        var winnerBoard = new WinnerBoard
            {
            CompetitionId = winnerBoardDto.CompetitionId,
            TeamId = winnerBoardDto.TeamId,
            Score = winnerBoardDto.Score
            };

        _context.WinnerBoards.Add(winnerBoard);
        await _context.SaveChangesAsync();

        winnerBoardDto.Id = winnerBoard.Id;

        return CreatedAtAction(nameof(GetAll), new { id = winnerBoard.Id }, winnerBoardDto);
        }
    }
