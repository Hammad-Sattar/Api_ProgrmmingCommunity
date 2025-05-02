using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_ProgrmmingCommunity.Models; // Replace with your actual namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/team/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllTeams()
            {
            return await _context.Teams
                .Where(t => t.IsDeleted == false || t.IsDeleted == null)
                .Select(t => new TeamDTO
                    {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName
                    })
                .ToListAsync();
            }

        [HttpGet("GetTeamIdByUserId/{userId}")]
        public IActionResult GetTeamIdByUserId(int userId)
            {
            var teamId = _context.TeamMembers
                .Where(tm => tm.UserId == userId && tm.IsDeleted == false)
                .OrderByDescending(tm => tm.Id)  // Orders by Id descending (most recent)
                .Select(tm => tm.TeamId)
                .FirstOrDefault();

            if (teamId == 0)
                {
                return NotFound("No team found for this user.");
                }

            return Ok(new { TeamId = teamId });
            }



        // GET: api/team/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeamById(int id)
            {
            var team = await _context.Teams
                .Where(t => t.TeamId == id && (t.IsDeleted == false || t.IsDeleted == null))
                .Select(t => new TeamDTO
                    {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName
                    })
                .FirstOrDefaultAsync();

            if (team == null)
                return NotFound();

            return team;
            }


        // POST: api/team/register
        [HttpPost("register")]
        public async Task<ActionResult<TeamDTO>> RegisterTeam([FromBody] TeamDTO dto)
            {
            var team = new Team
                {
                TeamName = dto.TeamName,
                IsDeleted = false
                };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            dto.TeamId = team.TeamId;

            return CreatedAtAction(nameof(GetTeamById), new { id = dto.TeamId }, dto);
            }

        // PUT: api/team/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamDTO dto)
            {
            if (id != dto.TeamId)
                return BadRequest();

            var team = await _context.Teams.FindAsync(id);

            if (team == null || team.IsDeleted == true)
                return NotFound();

            team.TeamName = dto.TeamName;
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            }

        // DELETE: api/team/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
            {
            var team = await _context.Teams.FindAsync(id);
            if (team == null || team.IsDeleted == true)
                return NotFound();

            team.IsDeleted = true;
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            }
        }
    }
