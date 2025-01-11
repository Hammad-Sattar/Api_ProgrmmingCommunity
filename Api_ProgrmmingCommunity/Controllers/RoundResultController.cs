using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundResultsController : ControllerBase
    {
        private readonly ProgrammingCommunityContext _context;

        public RoundResultsController(ProgrammingCommunityContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetRoundResults()
        {
            var roundResults = await _context.RoundResults
                .Include(r => r.CompetitionRound)
                .Include(r => r.User)
                .ToListAsync();

            if (!roundResults.Any())
                return NotFound("No round results found.");

            var roundResultsDto = roundResults.Select(r => new RoundResultDTO
            {
                Id = r.Id,
                CompetitionRoundId = r.CompetitionRoundId,
                UserId = r.UserId,
                Score = r.Score,
                IsQualified = r.IsQualified ?? false,
                CompetitionId = r.CompetitionId
            }).ToList();

            return Ok(new { Message = "Round results fetched successfully.", Data = roundResultsDto });
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoundResult(int id)
        {
            var roundResult = await _context.RoundResults
                .Include(r => r.CompetitionRound)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (roundResult == null)
                return NotFound("Round result not found.");

            var roundResultDto = new RoundResultDTO
            {
                Id = roundResult.Id,
                CompetitionRoundId = roundResult.CompetitionRoundId,
                UserId = roundResult.UserId,
                Score = roundResult.Score,
                IsQualified = roundResult.IsQualified ?? false,
                CompetitionId = roundResult.CompetitionId
            };

            return Ok(new { Message = "Round result fetched successfully.", Data = roundResultDto });
        }

       
        [HttpPost]
        public async Task<IActionResult> PostRoundResult(RoundResultDTO roundResultDTO)
        {
            if (roundResultDTO == null)
                return BadRequest("RoundResult data is required.");

            var roundResult = new RoundResult
            {
                CompetitionRoundId = roundResultDTO.CompetitionRoundId,
                UserId = roundResultDTO.UserId,
                Score = roundResultDTO.Score,
                IsQualified = roundResultDTO.IsQualified,
                CompetitionId = roundResultDTO.CompetitionId
            };

            _context.RoundResults.Add(roundResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoundResult), new { id = roundResult.Id }, new { Message = "Round result posted successfully.", Data = roundResultDTO });
        }

        
        [HttpGet("GetByRoundAndCompetition")]
        public async Task<IActionResult> GetRoundResultsByRoundAndCompetition(int roundId, int competitionId)
        {
            var roundResults = await _context.RoundResults
                .Include(r => r.CompetitionRound)
                .Include(r => r.User)
                .Where(r => r.CompetitionRoundId == roundId && r.CompetitionId == competitionId)
                .ToListAsync();

            if (!roundResults.Any())
                return NotFound("No round results found for the specified round and competition.");

            var roundResultsDto = roundResults.Select(r => new RoundResultDTO
            {
                Id = r.Id,
                CompetitionRoundId = r.CompetitionRoundId,
                UserId = r.UserId,
                Score = r.Score,
                IsQualified = r.IsQualified ?? false,
                CompetitionId = r.CompetitionId
            }).ToList();

            return Ok(new
            {
                Message = "Round results fetched successfully.",
                Data = roundResultsDto
            });
        }

    }
}
