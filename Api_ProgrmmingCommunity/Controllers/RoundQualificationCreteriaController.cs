using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class RoundQualificationCriteriaController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public RoundQualificationCriteriaController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("CreateRoundQualificationCriteria")]
        public IActionResult CreateRoundQualificationCriteria([FromBody] RoundQualificationCreteriaDTO roundQualificationCreteriaDto)
            {
            if (roundQualificationCreteriaDto == null)
                {
                return BadRequest("Round qualification criteria data is null.");
                }

            var roundQualificationCreteria = new RoundQualificationCriterion
                {
                FromRoundId = roundQualificationCreteriaDto.FromRoundId,
                ToRoundId = roundQualificationCreteriaDto.ToRoundId,
                TopTeams = roundQualificationCreteriaDto.TopTeams,
            
                };
            if(roundQualificationCreteria!=null)

            _context.RoundQualificationCriteria.Add(roundQualificationCreteria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoundQualificationCriteria), new { id = roundQualificationCreteria.Id }, roundQualificationCreteria);
            }

        [HttpGet("GetRoundQualificationCriteria")]
        public IActionResult GetRoundQualificationCriteria([FromQuery] int? id, [FromQuery] int? fromRoundId, [FromQuery] int? toRoundId)
            {
            var roundQualificationCreteria = _context.RoundQualificationCriteria
                .FirstOrDefault(rqc =>
                    (id != null && rqc.Id == id) ||
                    (fromRoundId != null && rqc.FromRoundId == fromRoundId) ||
                    (toRoundId != null && rqc.ToRoundId == toRoundId));

            if (roundQualificationCreteria == null)
                {
                return NotFound("Round qualification criteria not found.");
                }

            var roundQualificationCreteriaDto = new RoundQualificationCreteriaDTO
                {
                Id = roundQualificationCreteria.Id,
                FromRoundId = roundQualificationCreteria.FromRoundId,
                ToRoundId = roundQualificationCreteria.ToRoundId,
                TopTeams = roundQualificationCreteria.TopTeams,
              
                };

            return Ok(roundQualificationCreteriaDto);
            }

        [HttpPut("UpdateRoundQualificationCriteria")]
        public IActionResult UpdateRoundQualificationCriteria(int id, [FromBody] RoundQualificationCreteriaDTO roundQualificationCreteriaDto)
            {
            var roundQualificationCreteria = _context.RoundQualificationCriteria.FirstOrDefault(rqc => rqc.Id == id);

            if (roundQualificationCreteria == null)
                {
                return NotFound("Round qualification criteria not found.");
                }

            roundQualificationCreteria.FromRoundId = roundQualificationCreteriaDto.FromRoundId;
            roundQualificationCreteria.ToRoundId = roundQualificationCreteriaDto.ToRoundId;
            roundQualificationCreteria.TopTeams = roundQualificationCreteriaDto.TopTeams;
          

            _context.RoundQualificationCriteria.Update(roundQualificationCreteria);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteRoundQualificationCriteria")]
        public IActionResult DeleteRoundQualificationCriteria([FromQuery] int? id, [FromQuery] int? fromRoundId, [FromQuery] int? toRoundId)
            {
            var roundQualificationCreteria = _context.RoundQualificationCriteria
                .FirstOrDefault(rqc =>
                    (id != null && rqc.Id == id) ||
                    (fromRoundId != null && rqc.FromRoundId == fromRoundId) ||
                    (toRoundId != null && rqc.ToRoundId == toRoundId));

            if (roundQualificationCreteria == null)
                {
                return NotFound("Round qualification criteria not found.");
                }

            _context.RoundQualificationCriteria.Remove(roundQualificationCreteria);
            _context.SaveChanges();

            return Ok("Round qualification criteria deleted.");
            }
        }
    }
