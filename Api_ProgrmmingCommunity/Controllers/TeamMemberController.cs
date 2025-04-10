using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models; // Adjust to your namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public TeamMemberController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        // ✅ GET: api/teammember/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TeamMemberDTO>>> GetAllTeamMembers()
            {
            return await _context.TeamMembers
                .Where(t => t.IsDeleted == false || t.IsDeleted == null)
                .Select(t => new TeamMemberDTO
                    {
                    Id = t.Id,
                    TeamId = t.TeamId,
                    UserId = t.UserId
                    })
                .ToListAsync();
            }

        // ✅ GET: api/teammember/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMemberDTO>> GetTeamMemberById(int id)
            {
            var teamMember = await _context.TeamMembers
                .Where(t => t.Id == id && (t.IsDeleted == false || t.IsDeleted == null))
                .Select(t => new TeamMemberDTO
                    {
                    Id = t.Id,
                    TeamId = t.TeamId,
                    UserId = t.UserId
                    })
                .FirstOrDefaultAsync();

            if (teamMember == null)
                return NotFound();

            return teamMember;
            }

        // ✅ POST: api/teammember/register
        [HttpPost("register")]
        public async Task<ActionResult<TeamMemberDTO>> RegisterTeamMember([FromBody] TeamMemberDTO dto)
            {
            var newMember = new TeamMember
                {
                TeamId = dto.TeamId,
                UserId = dto.UserId,
                IsDeleted = false
                };

            _context.TeamMembers.Add(newMember);
            await _context.SaveChangesAsync();

            dto.Id = newMember.Id;

            return CreatedAtAction(nameof(GetTeamMemberById), new { id = dto.Id }, dto);
            }

        // ✅ PUT: api/teammember/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] TeamMemberDTO dto)
            {
            if (id != dto.Id)
                return BadRequest();

            var existing = await _context.TeamMembers.FindAsync(id);

            if (existing == null || existing.IsDeleted == true)
                return NotFound();

            existing.TeamId = dto.TeamId;
            existing.UserId = dto.UserId;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            }

        // ✅ DELETE: api/teammember/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
            {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null || member.IsDeleted == true)
                return NotFound();

            member.IsDeleted = true;
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            }
        }
    }
