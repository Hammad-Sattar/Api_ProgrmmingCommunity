using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public TeamController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddTeam")]
        public IActionResult AddTeam([FromBody] TeamDTO teamDto)
            {
            if (teamDto == null)
                {
                return BadRequest("Team data is null.");
                }

            var team = new Team
                {
                TeamName = teamDto.TeamName,
                Member1Id = teamDto.Member1Id,
                Member2Id = teamDto.Member2Id,
                Member3Id = teamDto.Member3Id,
                IsDeleted = false
                };

            _context.Teams.Add(team);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
            }

        [HttpGet("GetTeam")]
        public IActionResult GetTeam([FromQuery] int? teamId)
            {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == teamId && t.IsDeleted == false);

            if (team == null)
                {
                return NotFound("Team not found or is marked as deleted.");
                }

            return Ok(new TeamDTO
                {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                Member1Id = team.Member1Id,
                Member2Id = team.Member2Id,
                Member3Id = team.Member3Id,
                IsDeleted = team.IsDeleted
                });
            }

        [HttpGet("GetAllTeams")]
        public IActionResult GetAllTeams()
            {
            var teams = _context.Teams
                .Where(t => t.IsDeleted == false)
                .Select(t => new TeamDTO
                    {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    Member1Id = t.Member1Id,
                    Member2Id = t.Member2Id,
                    Member3Id = t.Member3Id,
                    IsDeleted = t.IsDeleted
                    })
                .ToList();

            return Ok(teams);
            }

        [HttpDelete("DeleteTeam")]
        public IActionResult DeleteTeam([FromQuery] int teamId)
            {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == teamId);

            if (team == null)
                {
                return NotFound("Team not found.");
                }

            team.IsDeleted = true;
            _context.Teams.Update(team);
            _context.SaveChanges();

            return Ok("Team marked as deleted.");
            }
        }
    }
