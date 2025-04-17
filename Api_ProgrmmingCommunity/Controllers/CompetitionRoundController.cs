using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionRoundController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public CompetitionRoundController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

       
        [HttpGet("GetAllCompetitionRoounds")]
        public async Task<ActionResult<IEnumerable<CompetitionRoundDTO>>> GetAll()
            {
            var rounds = _context.CompetitionRounds
                .Select(r => new CompetitionRoundDTO
                    {
                    Id = r.Id,
                    CompetitionId = r.CompetitionId,
                    RoundNumber = r.RoundNumber,
                    RoundType = r.RoundType,
                    Date = r.Date,
                   
                    })
                .ToList();

            return Ok(rounds);
            }

        // GET: api/CompetitionRound/GetAllRoundsByCompetitionId/{competitionId}
        [HttpGet("GetAllRoundsByCompetitionId/{competitionId}")]
        public async Task<ActionResult<IEnumerable<CompetitionRoundDTO>>> GetAllRoundsByCompetitionId(int competitionId)
            {
            var rounds = await _context.CompetitionRounds
                .Where(r => r.CompetitionId == competitionId && r.IsDeleted == false)
                .OrderBy(r => r.RoundNumber)
                .Select(r => new CompetitionRoundDTO
                    {
                    Id = r.Id,
                    CompetitionId = r.CompetitionId,
                    RoundNumber = r.RoundNumber,
                    RoundType = r.RoundType,
                    Date = r.Date
                    })
                .ToListAsync();

            if (rounds == null || !rounds.Any())
                {
                return NotFound("No rounds found for this competition.");
                }

            return Ok(rounds);
            }

        // POST: api/CompetitionRound/Add
        [HttpPost("AddCompetitonRound")]
        public async Task<IActionResult> Add([FromBody] CompetitionRoundDTO model)
            {
            if (model == null)
                {
                return BadRequest("Invalid data.");
                }

            var round = new CompetitionRound
                {
                CompetitionId = model.CompetitionId,
                RoundNumber = model.RoundNumber,
                RoundType = model.RoundType,
                Date = model.Date,
             
                };

            _context.CompetitionRounds.Add(round);
            await _context.SaveChangesAsync();

            return Ok(new {  RoundId = round.Id });
            }

        // PUT: api/CompetitionRound/Update/{id}
        [HttpPut("UpdateCompetitionRound/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompetitionRoundDTO model)
            {
            if (id != model.Id)
                {
                return BadRequest("ID mismatch.");
                }

            var round = await _context.CompetitionRounds.FindAsync(id);
            if (round == null)
                {
                return NotFound();
                }

            round.CompetitionId = model.CompetitionId;
            round.RoundNumber = model.RoundNumber;
            round.RoundType = model.RoundType;
            round.Date = model.Date;
           

            await _context.SaveChangesAsync();

            return Ok(new { message = "Competition round updated successfully" });
            }

        // DELETE: api/CompetitionRound/Delete/{id}
        [HttpDelete("DeleteCompetitonRounds/{id}")]
        public async Task<IActionResult> Delete(int id)
            {
            var round = await _context.CompetitionRounds.FindAsync(id);
            if (round == null)
                {
                return NotFound();
                }

            _context.CompetitionRounds.Remove(round);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Competition round deleted successfully" });
            }
        }
    }
