using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class RoundResultController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public RoundResultController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("CreateRoundResult")]
        public IActionResult CreateRoundResult([FromBody] RoundResultDTO roundResultDto)
            {
            if (roundResultDto == null)
                {
                return BadRequest("Round result data is null.");
                }

            var roundResult = new RoundResult
                {
                CompetitionRoundId = roundResultDto.CompetitionRoundId,
                TeamId = roundResultDto.TeamId,
                Score = roundResultDto.Score,
                CompetitionId = roundResultDto.CompetitionId,
                IsQualified = roundResultDto.IsQualified,
               
                };

            _context.RoundResults.Add(roundResult);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoundResult), new { id = roundResult.Id }, roundResult);
            }

        [HttpGet("GetRoundResult")]
        public IActionResult GetRoundResult([FromQuery] int? id, [FromQuery] int? competitionRoundId, [FromQuery] int? teamId, [FromQuery] int? competitionId)
            {
            var roundResult = _context.RoundResults
                .FirstOrDefault(rr =>
                    (id != null && rr.Id == id) ||
                    (competitionRoundId != null && rr.CompetitionRoundId == competitionRoundId) ||
                    (teamId != null && rr.TeamId == teamId) ||
                    (competitionId != null && rr.CompetitionId == competitionId));

            if (roundResult == null)
                {
                return NotFound("Round result not found.");
                }

            var roundResultDto = new RoundResultDTO
                {
                Id = roundResult.Id,
                CompetitionRoundId = roundResult.CompetitionRoundId,
                TeamId = roundResult.TeamId,
                Score = roundResult.Score,
                CompetitionId = roundResult.CompetitionId,
                IsQualified = roundResult.IsQualified,
               
                };

            return Ok(roundResultDto);
            }

        [HttpPut("UpdateRoundResult")]
        public IActionResult UpdateRoundResult(int id, [FromBody] RoundResultDTO roundResultDto)
            {
            var roundResult = _context.RoundResults.FirstOrDefault(rr => rr.Id == id);

            if (roundResult == null)
                {
                return NotFound("Round result not found.");
                }

            roundResult.CompetitionRoundId = roundResultDto.CompetitionRoundId;
            roundResult.TeamId = roundResultDto.TeamId;
            roundResult.Score = roundResultDto.Score;
            roundResult.CompetitionId = roundResultDto.CompetitionId;
            roundResult.IsQualified = roundResultDto.IsQualified;
           

            _context.RoundResults.Update(roundResult);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteRoundResult")]
        public IActionResult DeleteRoundResult([FromQuery] int? id, [FromQuery] int? competitionRoundId, [FromQuery] int? teamId, [FromQuery] int? competitionId)
            {
            var roundResult = _context.RoundResults
                .FirstOrDefault(rr =>
                    (id != null && rr.Id == id) ||
                    (competitionRoundId != null && rr.CompetitionRoundId == competitionRoundId) ||
                    (teamId != null && rr.TeamId == teamId) ||
                    (competitionId != null && rr.CompetitionId == competitionId));

            if (roundResult == null)
                {
                return NotFound("Round result not found.");
                }

            _context.RoundResults.Remove(roundResult);
            _context.SaveChanges();

            return Ok("Round result deleted.");
            }
        }
    }
