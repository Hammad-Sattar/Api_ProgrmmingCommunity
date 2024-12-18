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

        // GET: api/RoundResults
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

        // GET: api/RoundResults/5
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

        // POST: api/RoundResults
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

        // PUT: api/RoundResults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoundResult(int id, RoundResultDTO roundResultDTO)
        {
            if (id != roundResultDTO.Id)
                return BadRequest("ID mismatch.");

            var existingRoundResult = await _context.RoundResults.FindAsync(id);

            if (existingRoundResult == null)
                return NotFound("Round result not found.");

            existingRoundResult.CompetitionRoundId = roundResultDTO.CompetitionRoundId;
            existingRoundResult.UserId = roundResultDTO.UserId;
            existingRoundResult.Score = roundResultDTO.Score;
            existingRoundResult.IsQualified = roundResultDTO.IsQualified;
            existingRoundResult.CompetitionId = roundResultDTO.CompetitionId;

            _context.RoundResults.Update(existingRoundResult);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Round result updated successfully." });
        }

        // DELETE: api/RoundResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoundResult(int id)
        {
            var roundResult = await _context.RoundResults.FindAsync(id);
            if (roundResult == null)
                return NotFound("Round result not found.");

            _context.RoundResults.Remove(roundResult);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Round result deleted successfully." });
        }
    }
}
