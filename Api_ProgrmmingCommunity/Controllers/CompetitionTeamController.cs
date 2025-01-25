using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionTeamController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public CompetitionTeamController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddCompetitionTeam")]
        public IActionResult AddCompetitionTeam([FromBody] CompetitionTeamDTO competitionTeamDto)
            {
            if (competitionTeamDto == null)
                {
                return BadRequest("CompetitionTeam data is null.");
                }

            var competitionTeam = new CompetitionTeam
                {
                CompetitionId = competitionTeamDto.CompetitionId,
                TeamId = competitionTeamDto.TeamId,
                IsDeleted = false
                };

            _context.CompetitionTeams.Add(competitionTeam);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCompetitionTeam), new { id = competitionTeam.Id }, competitionTeam);
            }

        [HttpGet("GetCompetitionTeam")]
        public IActionResult GetCompetitionTeam([FromQuery] int competitionTeamId)
            {
            var competitionTeam = _context.CompetitionTeams
                .FirstOrDefault(ct => ct.Id == competitionTeamId && ct.IsDeleted == false);

            if (competitionTeam == null)
                {
                return NotFound("CompetitionTeam not found or is marked as deleted.");
                }

            return Ok(new CompetitionTeamDTO
                {
                Id = competitionTeam.Id,
                CompetitionId = competitionTeam.CompetitionId,
                TeamId = competitionTeam.TeamId,
              
                });
            }

        [HttpGet("GetAllCompetitionTeams")]
        public IActionResult GetAllCompetitionTeams()
            {
            var competitionTeams = _context.CompetitionTeams
                .Where(ct => ct.IsDeleted == false)
                .Select(ct => new CompetitionTeamDTO
                    {
                    Id = ct.Id,
                    CompetitionId = ct.CompetitionId,
                    TeamId = ct.TeamId,
                    
                    })
                .ToList();

            return Ok(competitionTeams);
            }

        [HttpDelete("DeleteCompetitionTeam")]
        public IActionResult DeleteCompetitionTeam([FromQuery] int competitionTeamId)
            {
            var competitionTeam = _context.CompetitionTeams
                .FirstOrDefault(ct => ct.Id == competitionTeamId);

            if (competitionTeam == null)
                {
                return NotFound("CompetitionTeam not found.");
                }

            competitionTeam.IsDeleted = true;
            _context.CompetitionTeams.Update(competitionTeam);
            _context.SaveChanges();

            return Ok("CompetitionTeam marked as deleted.");
            }
        }
    }
